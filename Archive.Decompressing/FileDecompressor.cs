using Archive.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archive.Decompressing
{
    public class FileDecompressor : IFileDecompressor
    {
        private readonly ICompressedFileReader _compressedFileReader;
        private readonly ICompressingSettings _compressingSettings;
        private readonly Semaphore _sema;

        private int _activeThreadCounter = 0;

        public FileDecompressor(
            ICompressingSettings compressingSettings,
            ICompressedFileReader compressedFileReader)
        {
            _compressedFileReader = compressedFileReader ?? throw new ArgumentNullException(nameof(compressedFileReader));
            _compressingSettings = compressingSettings ?? throw new System.ArgumentNullException(nameof(compressingSettings));

            var semaphoreCount = _compressingSettings.MaximumThreadsToUse;
            _sema = new Semaphore(semaphoreCount, semaphoreCount);
        }

        public bool Decompress()
        {
            foreach(var bytes in _compressedFileReader.ReadByteBlocks())
            {
                StartProcessing(bytes);
            }

            SpinWait.SpinUntil(() => _activeThreadCounter == 0);

            return true;
        }

        private void StartProcessing(byte[] bytes)
        {
            
        }

        public void Dispose()
        {
            //TODO не совсем правильно тут его диспозить, ибо кто породил тот и должен убивать=> запилить фабрику
            _compressedFileReader.Dispose();
        }
    }
}
