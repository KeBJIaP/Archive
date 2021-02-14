using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
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
    public class BlockCompressor : BlockingWorker, IFileCompressor
    {
        private readonly IAppSettings _fileSettings;
        private readonly ICompressingSettings _compressingSettings;
        private readonly IOutputCompressedFileWriter _outputFileWriter;


        public BlockCompressor(
            IAppSettings fileSettings,
            ICompressingSettings compressingSettings,
            IOutputCompressedFileWriter outputFileWriter)
            : base(compressingSettings)
        {
            _fileSettings = fileSettings ?? throw new System.ArgumentNullException(nameof(fileSettings));
            _outputFileWriter = outputFileWriter ?? throw new ArgumentNullException(nameof(outputFileWriter));
            _compressingSettings = compressingSettings ?? throw new System.ArgumentNullException(nameof(compressingSettings));
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
            WaitAllThreads();

            return true;
        }

        protected override void ProcessDataBlock(BlockData compressData)
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _outputFileWriter.Dispose();
            }
        }
    }
}
