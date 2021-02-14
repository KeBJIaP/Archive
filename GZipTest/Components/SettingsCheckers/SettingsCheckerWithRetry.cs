using Archive.Application.Common;
using GZipTest.Components.Messaging;
using GZipTest.Components.UserInteraction;
using System;
using System.IO;

namespace GZipTest.Components.SettingsCheckers
{
    /// <summary>
    /// Проверяет настройки приложения и дает поправить ошибки если что не так
    /// </summary>
    internal class SettingsCheckerWithRetry : ISettingsChecker
    {
        private readonly IAppSettings _settings;
        private readonly IMessagesService _messageService;
        private readonly IUserInteractionsService _userInteractionService;

        public SettingsCheckerWithRetry(
            IAppSettings settings,
            IMessagesService messageService,
            IUserInteractionsService userInteractionService)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _userInteractionService = userInteractionService ?? throw new ArgumentNullException(nameof(userInteractionService));
        }

        public bool CheckSettings()
        {
            var settings = _settings;

            if (!CheckSourceFile(settings))
            {
                return false;
            }
            if (!CheckDestinationFile(settings))
            {
                return false;
            }

            return true;
        }

        private bool CheckDestinationFile(IAppSettings settings)
        {
            if (File.Exists(settings.ResultFile))
            {
                //Если результирующий файл уже присутствует - надо спросить удалить ли его
                _messageService.Message(String.Format(Properties.Resources.ResultFileExists, settings.ResultFile));

                if (_userInteractionService.AskYesNo(Properties.Resources.DeleteFile))
                {
                    File.Delete(settings.ResultFile);
                    if (!File.Exists(settings.ResultFile))
                    {
                        _messageService.Message(String.Format(Properties.Resources.FileHasBeenDeleted, settings.ResultFile));
                    }
                    else
                    {
                        _messageService.Message(String.Format(Properties.Resources.FailedToDeleteFile, settings.ResultFile));
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckSourceFile(IAppSettings settings)
        {
            if (!File.Exists(settings.SourceFile))
            {
                //Если нет исходного файла - проверка провалена.
                _messageService.Message(String.Format(Properties.Resources.SourceFileDoesNotExist, settings.SourceFile));
                return false;
            }
            return true;
        }
    }
}
