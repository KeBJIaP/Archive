using Archive.Application.Common;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace GZipTest.Settings
{
    internal class CalculatedCompressorSettings : ICompressingSettings
    {
        private readonly IBlockSizeStrategy _blockSizeStrategy;
        private readonly IThreadsCountStrategy _threadsCountStrategy;

        public int BytesToRead => _blockSizeStrategy.Calculate();

        public int MaximumThreadsToUse => _threadsCountStrategy.Calculate();

        public CalculatedCompressorSettings(
            IBlockSizeStrategy blockSizeStrategy,
            IThreadsCountStrategy threadsCountStrategy)
        {
            _blockSizeStrategy = blockSizeStrategy ?? throw new ArgumentNullException(nameof(blockSizeStrategy));
            _threadsCountStrategy = threadsCountStrategy ?? throw new ArgumentNullException(nameof(threadsCountStrategy));
        }
    }
}
