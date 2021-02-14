using Archive.Application.Common;
using Archive.Compressing;
using Archive.Decompressing;
using GZipTest.Components.Messaging;
using GZipTest.Components.SettingsCheckers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipTest
{
    /// <summary>
    /// Занимается запуском процессов компресси/декомпрессии в зависимости от настроек
    /// Является точкой входа и рещает когда программе завершиться
    /// </summary>
    internal class Archiver : IArchiver
    {
        private readonly ISettingsChecker _settingsChecker;
        private readonly IAppSettings _settings;
        private readonly IMessagesService _messageService;
        private readonly IDecompressorFactory _decompressorFactory;
        private readonly ICompressorFactory _compressorFactory;

        public Archiver(
            ISettingsChecker settingsChecker,
            IAppSettings settings,
            IMessagesService messageService,
            IDecompressorFactory decompressorFactory,
            ICompressorFactory compressorFactory)
        {
            _settingsChecker = settingsChecker ?? throw new ArgumentNullException(nameof(settingsChecker));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _decompressorFactory = decompressorFactory ?? throw new ArgumentNullException(nameof(decompressorFactory));
            _compressorFactory = compressorFactory ?? throw new ArgumentNullException(nameof(compressorFactory));
        }

        public int Start()
        {
            if (!_settingsChecker.CheckSettings())
            {
                _messageService.Message(Properties.Resources.UnableToProceed);
                //завершение программы потому что сеттинги плохие
                return 0;
            }
            switch (_settings.Mode)
            {
                case ApplicationMode.Compress:
                    {
                        //необходимо разделить контекст поэтому фабрика а не прямая зависимость
                        using (var compressor = _compressorFactory.Create())
                        {
                            if (compressor.Compress())
                            {
                                return 1;
                            }
                        }
                        return 0;
                    }

                case ApplicationMode.Decompress:
                    {
                        //необходимо разделить контекст поэтому фабрика а не прямая зависимость
                        using (var decompressor = _decompressorFactory.Create())
                        {
                            if (decompressor.Decompress())
                            {
                                return 1;
                            }
                        }
                        return 0;
                    }

                default:
                    return 0;
            }
        }
    }
}
