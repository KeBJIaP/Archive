namespace Archive.Compressing
{
    public interface ICompressorFactory
    {
        IFileCompressor Create();
    }
}
