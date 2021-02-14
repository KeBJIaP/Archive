namespace Archive.Decompressing
{
    public interface IDecompressedFileWriter
    {
        void QueueWrite(int blockNum, byte[] result);
    }
}