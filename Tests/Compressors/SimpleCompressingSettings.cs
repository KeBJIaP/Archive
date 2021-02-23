using Archive.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Compressors
{
    class SimpleCompressingSettings : ICompressingSettings
    {
        public int BytesToRead { get; set; }

        public int MaximumThreadsToUse { get; set; }
    }
}
