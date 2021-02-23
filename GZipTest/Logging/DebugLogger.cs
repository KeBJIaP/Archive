using Archive.Application.Common;
using System;

namespace GZipTest.Logging
{
    public class DebugLogger : ILogger
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warning(string message, Exception ex = null)
        {
            System.Diagnostics.Debug.WriteLine($"{message}\n{ex.ToString()}");
        }
    }
}
