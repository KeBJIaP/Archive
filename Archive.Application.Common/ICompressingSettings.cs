using System.IO.Compression;

namespace Archive.Application.Common
{
    public interface ICompressingSettings
    {
        int BytesToRead { get; }
        int MaximumThreadsToUse { get; }
    }
}
