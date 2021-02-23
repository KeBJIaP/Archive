using Archive.Application.Common;
using Archive.BlockedCompressing.Base;

namespace Archive.Decompressing
{
    public class DecompressedFileWriter : FileWriter, IDecompressedFileWriter
    {
        bool IDecompressedFileWriter.IsWriting => IsWriting;

        public DecompressedFileWriter(
            IAppSettings settings,
            IFileWriteStrategy writeStrategy)
            : base(settings, writeStrategy)
        {
        }
    }
}
