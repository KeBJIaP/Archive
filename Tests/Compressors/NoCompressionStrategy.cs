using Archive.Compressing.Compression;

namespace Tests.Compressors
{
    class NoCompressionStrategy : ICompressionStrategy
    {
        public byte[] Compress(byte[] bytesToCompress)
        {
            return bytesToCompress;
        }
    }
}
