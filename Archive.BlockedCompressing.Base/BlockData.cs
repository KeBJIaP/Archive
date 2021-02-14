using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.BlockedCompressing.Base
{
    public class BlockData
    {
        public int ThreadId { get; }
        public byte[] BytesToCompress { get; }
        public int BlockNum { get; }

        public BlockData(int threadId, byte[] bytesToCompress, int blockNum)
        {
            ThreadId = threadId;
            BytesToCompress = bytesToCompress;
            BlockNum = blockNum;
        }
    }
}
