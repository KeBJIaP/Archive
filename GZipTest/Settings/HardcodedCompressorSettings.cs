using Archive.Application.Common;
using System.IO.Compression;

namespace GZipTest.Settings
{
    public class HardcodedCompressorSettings : ICompressingSettings
    {
        public int BytesToRead { get; } = 40000;

        public int MaximumThreadsToUse { get; } = 4;

        public CompressionLevel CompressionLevel { get; } = CompressionLevel.Fastest;
    }
}
