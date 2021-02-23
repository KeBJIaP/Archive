using Archive.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipTest.Settings
{
    public class AppSettingsFromStartArguments : IAppSettings
    {
        private static readonly string CompressArgument = "compress";
        private static readonly string DecompressArgument = "decompress";

        private const int SourceFileArgsIndex = 1;
        private const int ResultFileArgsIndex = 2;
        private const int ModeArgsIndex = 0;

        private readonly IEqualityComparer<string> _stringComparer;

        public string SourceFile => GetArgs(SourceFileArgsIndex);

        public string ResultFile => GetArgs(ResultFileArgsIndex);

        public ApplicationMode Mode => GetMode();

        public AppSettingsFromStartArguments(IEqualityComparer<string> stringComparer)
        {
            _stringComparer = stringComparer ?? throw new ArgumentNullException(nameof(stringComparer));
        }

        private ApplicationMode GetMode()
        {
            //Режим работы
            var modeString = GetArgs(ModeArgsIndex);
            if (_stringComparer.Equals(modeString, CompressArgument))
            {
                return ApplicationMode.Compress;
            }
            else if (_stringComparer.Equals(modeString, DecompressArgument))
            {
                return ApplicationMode.Decompress;
            }
            else
            {
                throw new ModeParseException();
            }
        }

        private string GetArgs(int argsIndex)
        {
            var allArgs = Environment.GetCommandLineArgs();
            //GetCommandLineArgs возвращает запускаемый файл первым аргументом
            var args = allArgs.Skip(1).ToArray();
            if (args.Length < argsIndex + 1)
            {
                throw new ArgsLengthException();
            }
            return args[argsIndex];
        }
    }
}
