using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using System.Collections.Concurrent;
using System.IO;

namespace Archive.Compressing
{
    /// <summary>
    /// Записывает сжжатые данные по схеме: размер блока + блок
    /// Ожидает запись каждого блока по очереди
    /// </summary>
    public class OutputCompressedFileWriter : FileWriter, IOutputWriter
    {
        bool IOutputWriter.IsWriting => IsWriting;

        public OutputCompressedFileWriter(
            IAppSettings settings,
            IFileWriteStrategy writeStrategy)
            : base(settings, writeStrategy)
        {
        }
    }
}
