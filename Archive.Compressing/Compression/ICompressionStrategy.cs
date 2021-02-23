using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Compressing.Compression
{
    public interface ICompressionStrategy
    {
        byte[] Compress(byte[] bytesToCompress);
    }
}
