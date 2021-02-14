using Archive.Application.Common;
using Archive.Common.Containers;
using Archive.Common.Containers.UnityContainers;
using Archive.Compressing;
using Archive.Decompressing;
using GZipTest.Components.Messaging;
using GZipTest.Components.SettingsCheckers;
using GZipTest.Components.UserInteraction;
using GZipTest.Settings;
using System;
using System.Collections.Generic;

namespace GZipTest
{
    internal class ArchiverUnityFactory
    {
        public IArchiver Create()
        {
            using (IDependenciesContainer container = new UnityDependenciesContainer())
            {
                //settings
                container.RegisterExternallyControlledSingletone<IAppSettings, HardcodedCompressAppSettings>();
                container.Register<ICompressingSettings, HardcodedCompressorSettings>();

                container.RegisterInstance<IEqualityComparer<string>>(StringComparer.InvariantCultureIgnoreCase);

                container.Register<ISettingsChecker, SettingsCheckerWithRetry>();
                container.Register<IMessagesService, ConsoleMessageService>();
                container.Register<IUserInteractionsService, ConsoleUserInteractionsService>();

                //compressor
                container.Register<ICompressorFactory, BlockedCompressorFactory>();
                container.Register<IDecompressorFactory, BlockDecompressorFactory>();

                return container.Resolve<Archiver>();
            }
        }
    }
}