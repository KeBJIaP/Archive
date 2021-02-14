using System.IO;

namespace Archive.BlockedCompressing.Base
{
    public interface IFileWriteStrategy
    {
        void Write(BinaryWriter bw, byte[] bytes);
    }
}
