namespace Archive.Decompressing
{
    public interface IDecompressorFactory
    {
        IFileDecompressor Create();
    }
}
