using Archive.Compressing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tests.Compressors
{
    class WaitWriterWithCounting : IOutputWriter
    {
        private int total = 0;

        private int _blocksReceived;
        public int BlocksReceived => _blocksReceived;

        public bool IsWriting => total > 0;

        public void QueueWrite(int blockNum, byte[] bytes)
        {
            Interlocked.Increment(ref total);
            Interlocked.Increment(ref _blocksReceived);
            Interlocked.Decrement(ref total);
        }

        public void Dispose()
        {
        }
    }
}
