using Archive.Application.Common;
using System;
using System.Threading;

namespace Archive.BlockedCompressing.Base
{
    public abstract class BlockingWorker : IDisposable
    {
        private readonly ICompressingSettings _compressingSettings;

        private readonly Semaphore _sema;
        private int _activeThreadCounter = 0;

        protected BlockingWorker(ICompressingSettings compressingSettings)
        {
            _compressingSettings = compressingSettings ?? throw new System.ArgumentNullException(nameof(compressingSettings));

            var semaphoreCount = _compressingSettings.MaximumThreadsToUse;
            _sema = new Semaphore(semaphoreCount, semaphoreCount);
        }

        protected void StartProcessing(byte[] bytes, int blockNumber)
        {
            Interlocked.Increment(ref _activeThreadCounter);
            //синхронизируем по количеству потоков
            if (_sema.WaitOne())
            {
                var thread = new Thread(new ParameterizedThreadStart(Process));
                var threadContext = new BlockData(thread.ManagedThreadId, bytes, blockNumber);
                thread.Start(threadContext);
            }
        }

        protected void WaitAllThreads()
        {
            _sema.WaitOne();
            SpinWait.SpinUntil(() => _activeThreadCounter == 0);
            _sema.Release();
        }

        private void Process(object obj)
        {
            try
            {
                ProcessDataBlock((BlockData)obj);
            }
            finally
            {
                _sema.Release();
                Interlocked.Decrement(ref _activeThreadCounter);
            }
        }

        protected abstract void ProcessDataBlock(BlockData obj);

        #region Dispose
        ~BlockingWorker()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sema.Dispose();
            }
        }

        #endregion
    }
}
