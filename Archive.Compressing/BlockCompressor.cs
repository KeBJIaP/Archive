using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using Archive.Compressing.CompressingSource;
using Archive.Compressing.Compression;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Archive.Compressing
{
    /// <summary>
    /// Поблочно сжимает файл
    /// Не переиспользуется
    /// </summary>
    public class BlockCompressor : BlockingWorker, IFileCompressor
    {
        private readonly IOutputWriter _outputFileWriter;
        private readonly ILogger _logger;
        private readonly IBlocksToCompressSource _inputBlocksSource;
        private readonly ICompressionStrategy _blockCompressionStrategy;

        public BlockCompressor(
            ICompressingSettings compressingSettings,
            IOutputWriter outputFileWriter,
            ILogger logger,
            IBlocksToCompressSource inputBlocksSource,
            ICompressionStrategy blockCompressionStrategy)
                : base(compressingSettings)
        {
            _outputFileWriter = outputFileWriter ?? throw new ArgumentNullException(nameof(outputFileWriter));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _inputBlocksSource = inputBlocksSource ?? throw new ArgumentNullException(nameof(inputBlocksSource));
            _blockCompressionStrategy = blockCompressionStrategy ?? throw new ArgumentNullException(nameof(blockCompressionStrategy));
        }

        public bool Compress()
        {
            var index = 0;

            foreach (var block in _inputBlocksSource.ReadAllBlocks())
            {
                StartProcessing(block, index++);
            }

            //Ждем пока все потоки закончат
            WaitAllThreads();
            //Ждем пока очередь записи в файл кончится
            SpinWait.SpinUntil(() => !_outputFileWriter.IsWriting);
            _logger.Debug($"При выходе из компрессора index = {index}");

            return true;
        }

        protected override void ProcessDataBlock(BlockData compressData)
        {
            var result = _blockCompressionStrategy.Compress(compressData.Bytes);
            _outputFileWriter.QueueWrite(compressData.BlockNum, result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _outputFileWriter.Dispose();
            }
        }
    }
}
