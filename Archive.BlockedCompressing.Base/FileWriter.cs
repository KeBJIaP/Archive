using Archive.Application.Common;
using Archive.BlockedCompressing.Base.Exceptions;
using System.Collections.Concurrent;
using System.IO;

namespace Archive.BlockedCompressing.Base
{
    /// <summary>
    /// Записывает сжжатые данные по схеме: размер блока + блок
    /// Ожидает запись каждого блока по очереди
    /// </summary>
    public class FileWriter
    {
        private readonly ConcurrentDictionary<int, byte[]> _dict = new ConcurrentDictionary<int, byte[]>();
        private readonly FileStream _fileStream;
        private readonly BinaryWriter _binaryWriter;
        private readonly IFileWriteStrategy _writeStrategy;
        private int _currentPosition = 0;

        public FileWriter(IAppSettings settings, IFileWriteStrategy writeStrategy)
        {
            _writeStrategy = writeStrategy ?? throw new System.ArgumentNullException(nameof(writeStrategy));

            _fileStream = File.OpenWrite(settings.ResultFile);
            _binaryWriter = new BinaryWriter(_fileStream);
        }

        public void QueueWrite(int blockNum, byte[] bytes)
        {
            //Добавляем блок на запись
            if (!_dict.TryAdd(blockNum, bytes))
            {
                throw new WriteFileException();
            }
            //пыиаемся процессить следующий блок
            TryProcessNext();
        }

        private void TryProcessNext()
        {
            if (_dict.TryRemove(_currentPosition, out var bytes))
            {
                //удалось получить следующий блок
                _writeStrategy.Write(_binaryWriter, bytes);

                _fileStream.Flush();
                _currentPosition++;
            }
        }

        public void Dispose()
        {
            _fileStream?.Dispose();
            _binaryWriter?.Dispose();
        }
    }
}
