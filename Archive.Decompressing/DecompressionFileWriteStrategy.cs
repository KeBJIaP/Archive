using Archive.BlockedCompressing.Base;
using System.IO;

namespace Archive.Decompressing
{
    public class DecompressionFileWriteStrategy : IFileWriteStrategy
    {
        public void Write(BinaryWriter bw, byte[] bytes)
        {
            bw.Write(bytes);
        }
    }
}