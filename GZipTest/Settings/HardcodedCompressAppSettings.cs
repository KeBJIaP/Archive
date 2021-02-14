using Archive.Application.Common;
using System.IO;

namespace GZipTest.Settings
{
    internal class HardcodedCompressAppSettings : IAppSettings
    {
        public string SourceFile { get; }

        public string ResultFile { get; }

        public ApplicationMode Mode { get; }

        public HardcodedCompressAppSettings()
        {
            //SourceFile = Path.Combine(System.Environment.CurrentDirectory, "examples", "123.txt");
            //ResultFile = Path.Combine(System.Environment.CurrentDirectory, "examples", "Result.arch");
            //Mode = ApplicationMode.Compress;

            SourceFile = Path.Combine(System.Environment.CurrentDirectory, "examples", "Result.arch");
            ResultFile = Path.Combine(System.Environment.CurrentDirectory, "examples", "222.txt");
            Mode = ApplicationMode.Decompress;
        }
    }
}
