using Archive.Application.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Archive.Compressing
{
    /// <summary>
    /// Поблочно сжимает файл
    /// Не переиспользуется
    /// </summary>
    public class BlockCompressor : IFileCompressor
    {
        private readonly IAppSettings _fileSettings;
        private readonly ICompressingSettings _compressingSettings;
        private readonly IOutputCompressedFileWriter _outputFileWriter;
        private readonly Semaphore _sema;

        private int _activeThreadCounter = 0;

        public BlockCompressor(
            IAppSettings fileSettings,
            ICompressingSettings compressingSettings,
            IOutputCompressedFileWriter outputFileWriter)
        {
            _fileSettings = fileSettings ?? throw new System.ArgumentNullException(nameof(fileSettings));
            _outputFileWriter = outputFileWriter ?? throw new ArgumentNullException(nameof(outputFileWriter));
            _compressingSettings = compressingSettings ?? throw new System.ArgumentNullException(nameof(compressingSettings));

            var semaphoreCount = _compressingSettings.MaximumThreadsToUse;
            _sema = new Semaphore(semaphoreCount, semaphoreCount);
        }

        public bool Compress()
        {
            //TODO вынести чтение в зависимость
            //Открываем стрим файла и обрабатываем блоки
            using (var fileStream = File.OpenRead(_fileSettings.SourceFile))
            using (var br = new BinaryReader(fileStream))
            {
                var index = 0;
                while (br.PeekChar() != -1)
                {
                    //читаем блоки по сколько-то там байт
                    var bytes = br.ReadBytes(_compressingSettings.BytesToRead);

                    StartProcessing(bytes, index++);
                }
            }

            //Ждем пока все потоки закончат
            SpinWait.SpinUntil(() => _activeThreadCounter == 0);

            return true;
        }

        private void StartProcessing(byte[] bytes, int blockNumber)
        {
            //TPL нельзя
            //синхронизируем по количеству потоков
            if (_sema.WaitOne())
            {
                var thread = new Thread(new ParameterizedThreadStart(DoCompressAndQueue));
                var threadContext = new CompressData(thread.ManagedThreadId, bytes, blockNumber);
                Interlocked.Increment(ref _activeThreadCounter);
                thread.Start(threadContext);
            }
        }

        private void DoCompressAndQueue(object obj)
        {
            var compressData = (CompressData)obj;
            try
            {
                //берем пачку данных, компрессим и суем в очередь на запись в выходной файл
                using (var dataToCompressStream = new MemoryStream(compressData.BytesToCompress))
                {
                    var dataToWriteStream = new MemoryStream();
                    var br = new BinaryReader(dataToWriteStream);
                    //gzipStream надо закрывать первым
                    using (var gzipStream = new GZipStream(dataToWriteStream, CompressionMode.Compress))
                    {
                        dataToCompressStream.CopyTo(gzipStream);
                        gzipStream.Flush();
                    }

                    var result = dataToWriteStream.ToArray();

                    //получили порцию данных - записали в выходной файл
                    _outputFileWriter.QueueWrite(compressData.BlockNum, result);

                    dataToWriteStream.Flush();
                    br.Dispose();
                    dataToWriteStream.Dispose();
                }
            }
            finally
            {
                _sema.Release();
                Interlocked.Decrement(ref _activeThreadCounter);
            }
        }

        #region Dispose
        ~BlockCompressor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sema.Dispose();
                _outputFileWriter.Dispose();
            }
        }
        #endregion

        private class CompressData
        {
            public int ThreadId { get; }
            public byte[] BytesToCompress { get; }
            public int BlockNum { get; }

            public CompressData(int threadId, byte[] bytesToCompress, int blockNum)
            {
                ThreadId = threadId;
                BytesToCompress = bytesToCompress;
                BlockNum = blockNum;
            }
        }
    }
}
