using Archive.Application.Common;
using System.IO;

namespace GZipTest.Settings
{
    internal class HardcodedCompressAppSettings : IAppSettings
    {
        public string SourceFile { get; }

        public string ResultFile { get; }

        public ApplicationMode Mode { get; } = ApplicationMode.Compress;

        public HardcodedCompressAppSettings()
        {
            SourceFile = Path.Combine(System.Environment.CurrentDirectory, "examples", "ToArchive.bin");
            ResultFile = Path.Combine(System.Environment.CurrentDirectory, "examples", "Result.arch");
        }
    }
}
