using Archive.Application.Common;
using Archive.Common.Containers;
using Archive.Common.Containers.UnityContainers;
using Archive.Compressing;
using Archive.Compressing.CompressingSource;
using Archive.Decompressing;
using GZipTest.Components.Messaging;
using GZipTest.Components.SettingsCheckers;
using GZipTest.Components.UserInteraction;
using GZipTest.Logging;
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
                container.Register<ILogger, DebugLogger>();
                //settings
                container.Register<IBlockSizeStrategy, ByRamBlockSizeStrategy>();
                container.Register<IThreadsCountStrategy, ByCpuThreadsCountStrategy>();

                container.RegisterExternallyControlledSingletone<IAppSettings, AppSettingsFromStartArguments>();
                container.Register<ICompressingSettings, CalculatedCompressorSettings>();

                container.RegisterInstance<IEqualityComparer<string>>(StringComparer.InvariantCultureIgnoreCase);

                container.Register<ISettingsChecker, SettingsCheckerWithRetry>();
                container.Register<IMessagesService, ConsoleMessageService>();
                container.Register<IUserInteractionsService, ConsoleUserInteractionsService>();

                //compressor                
                container.Register<ICompressorFactory, FileBlockedCompressorFactory>();
                container.Register<IDecompressorFactory, BlockDecompressorFactory>();

                return container.Resolve<Archiver>();
            }
        }
    }
}