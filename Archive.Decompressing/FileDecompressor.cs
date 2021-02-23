using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using Archive.Compressing.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archive.Decompressing
{
    public class FileDecompressor : BlockingWorker, IFileDecompressor
    {
        private readonly ICompressedFileReader _compressedFileReader;
        private readonly ICompressingSettings _compressingSettings;
        private readonly IDecompressedFileWriter _decompressedFileWriter;
        private readonly ILogger _logger;
        private readonly IDecompressionStrategy _blockDecompressionStrategy;

        public FileDecompressor(
            ICompressingSettings compressingSettings,
            ICompressedFileReader compressedFileReader,
            IDecompressedFileWriter decompressedFileWriter,
            IDecompressionStrategy blockDecompressionStrategy,
            ILogger logger)
                : base(compressingSettings)
        {
            _compressedFileReader = compressedFileReader ?? throw new ArgumentNullException(nameof(compressedFileReader));
            _compressingSettings = compressingSettings ?? throw new System.ArgumentNullException(nameof(compressingSettings));
            _decompressedFileWriter = decompressedFileWriter;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _blockDecompressionStrategy = blockDecompressionStrategy ?? throw new ArgumentNullException(nameof(blockDecompressionStrategy));
        }

        public bool Decompress()
        {
            int index = 0;
            foreach (var bytes in _compressedFileReader.ReadByteBlocks())
            {
                _logger.Debug($"Обрабатываем блок {index}");
                StartProcessing(bytes, index++);
            }
            //Ждем пока все потоки обработки закончат работу
            WaitAllThreads();
            //Ждем пока очередь записи в файл кончится
            SpinWait.SpinUntil(() => !_decompressedFileWriter.IsWriting);

            _logger.Debug($"При выходе из декомпрессора index = {index}");

            return true;
        }

        protected override void ProcessDataBlock(BlockData block)
        {
            var result = _blockDecompressionStrategy.Decompress(block.Bytes);

            _decompressedFileWriter.QueueWrite(block.BlockNum, result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
