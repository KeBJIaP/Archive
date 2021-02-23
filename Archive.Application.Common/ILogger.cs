using System;

namespace Archive.Application.Common
{
    public interface ILogger
    {
        void Debug(string message);
        void Warning(string message, Exception ex = null);
    }
}
