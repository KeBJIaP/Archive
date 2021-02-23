using System.Collections.Generic;

namespace Archive.Compressing.CompressingSource
{
    public interface IBlocksToCompressSource
    {
        IEnumerable<byte[]> ReadAllBlocks();
    }
}
