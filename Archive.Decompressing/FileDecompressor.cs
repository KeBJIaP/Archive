using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
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

        public FileDecompressor(
            ICompressingSettings compressingSettings,
            ICompressedFileReader compressedFileReader,
            IDecompressedFileWriter decompressedFileWriter)
            : base(compressingSettings)
        {
            _compressedFileReader = compressedFileReader ?? throw new ArgumentNullException(nameof(compressedFileReader));
            _compressingSettings = compressingSettings ?? throw new System.ArgumentNullException(nameof(compressingSettings));
            _decompressedFileWriter = decompressedFileWriter;
        }

        public bool Decompress()
        {
            int index = 0;
            foreach (var bytes in _compressedFileReader.ReadByteBlocks())
            {
                StartProcessing(bytes, index++);
            }

            WaitAllThreads();

            return true;
        }

        protected override void ProcessDataBlock(BlockData block)
        {
            var data = block.BytesToCompress;
            var ms = new MemoryStream(data);
            var outputStream = new MemoryStream();

            using (var gzipStream = new GZipStream(ms, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(outputStream);
                gzipStream.Flush();
            }

            //прочитали порцию данных
            var result = outputStream.ToArray();
            //можно и записать в выходную штуку
            _decompressedFileWriter.QueueWrite(block.BlockNum, result);

            ms.Dispose();
            outputStream.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
