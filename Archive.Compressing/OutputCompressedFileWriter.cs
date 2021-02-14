using Archive.Application.Common;
using System.Collections.Concurrent;
using System.IO;

namespace Archive.Compressing
{
    /// <summary>
    /// Записывает сжжатые данные по схеме: размер блока + блок
    /// Ожидает запись каждого блока по очереди
    /// </summary>
    public class OutputCompressedFileWriter : IOutputCompressedFileWriter
    {
        private readonly ConcurrentDictionary<int, byte[]> _dict = new ConcurrentDictionary<int, byte[]>();
        private readonly FileStream _fileStream;

        private int _currentPosition = 0;

        public OutputCompressedFileWriter(IAppSettings settings)
        {
            _fileStream = File.OpenWrite(settings.ResultFile);
        }

        public void QueueWrite(int blockNum, byte[] bytes)
        {
            //Добавляем блок на запись
            if (!_dict.TryAdd(blockNum, bytes))
            {
                throw new CompressionException();
            }
            //пыиаемся процессить следующий блок
            TryProcessNext();
        }

        private void TryProcessNext()
        {
            if (_dict.TryRemove(_currentPosition, out var bytes))
            {
                using (var bw = new BinaryWriter(_fileStream))
                {
                    //удалось получить следующий блок
                    bw.Write(bytes.Length);
                    bw.Write(bytes);
                }
            }
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
