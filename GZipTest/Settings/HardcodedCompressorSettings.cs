using Archive.Application.Common;
using System.IO.Compression;

namespace GZipTest.Settings
{
    public class HardcodedCompressorSettings : ICompressingSettings
    {
        public int BytesToRead { get; } = 3000;

        public int MaximumThreadsToUse { get; } = 1;

        public CompressionLevel CompressionLevel { get; } = CompressionLevel.Fastest;
    }
}
