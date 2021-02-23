namespace Archive.Decompressing
{
    public interface IDecompressedFileWriter
    {
        bool IsWriting { get; }
        void QueueWrite(int blockNum, byte[] result);
    }
}