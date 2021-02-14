using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Decompressing
{
    public class DecompressedFileWriter : FileWriter, IDecompressedFileWriter
    {
        public DecompressedFileWriter(
            IAppSettings settings, 
            IFileWriteStrategy writeStrategy) 
            : base(settings, writeStrategy)
        {
        }
    }
}
