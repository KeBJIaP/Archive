using System;

namespace Archive.Decompressing
{
    public interface IFileDecompressor : IDisposable
    {
        bool Decompress();
    }
}