using System;
using System.Collections.Generic;

namespace Archive.Decompressing
{
    public interface ICompressedFileReader : IDisposable
    {
        IEnumerable<byte[]> ReadByteBlocks();
    }
}