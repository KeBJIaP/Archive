using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using System.IO;

namespace Archive.Compressing
{
    public class CompressionFileWriteStrategy : IFileWriteStrategy
    {
        private readonly ILogger _logger;
        int count = 0;

        public CompressionFileWriteStrategy(ILogger logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public void Write(BinaryWriter bw, byte[] bytes)
        {
            bw.Write(bytes.Length);
            bw.Write(bytes);
            _logger.Debug($"Written block {count++}");
        }
    }
}
