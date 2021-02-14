using Archive.BlockedCompressing.Base;
using System.IO;

namespace Archive.Compressing
{
    public class CompressionFileWriteStrategy : IFileWriteStrategy
    {
        public void Write(BinaryWriter bw, byte[] bytes)
        {
            bw.Write(bytes.Length);
            bw.Write(bytes);
        }
    }
}
