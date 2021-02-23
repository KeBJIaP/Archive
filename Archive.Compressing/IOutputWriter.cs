using System;

namespace Archive.Compressing
{
    /// <summary>
    /// Пишет в выходной файл компресснутые данные.
    /// Пишет по очереди, ждет все блоки
    /// </summary>
    public interface IOutputWriter : IDisposable
    {
        bool IsWriting { get; }

        void QueueWrite(int blockNum, byte[] bytes);
    }
}