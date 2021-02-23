using Archive.Compressing.CompressingSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Compressors
{
    class HardcodedBlocksSource : IBlocksToCompressSource
    {
        private readonly byte[][] _blocks;

        public HardcodedBlocksSource(byte[][] blocks)
        {
            this._blocks = blocks;
        }

        public IEnumerable<byte[]> ReadAllBlocks()
        {
            return _blocks.ToList();
        }
    }
}
