using System;

namespace Archive.Compressing
{
    public interface IFileCompressor : IDisposable
    {
        bool Compress();
    }
}
