namespace Archive.Application.Common
{
    public interface IAppSettings
    {
        string SourceFile { get; }
        string ResultFile { get; }
        ApplicationMode Mode { get; }
    }
}
