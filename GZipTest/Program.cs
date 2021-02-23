using Archive.Application.Common;
using Archive.Common.Containers.UnityContainers;
using GZipTest.Components.Messaging;
using GZipTest.Components.SettingsCheckers;
using GZipTest.Components.UserInteraction;
using GZipTest.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipTest
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var arch = new ArchiverUnityFactory().Create();
                return arch.Start();
            }
            catch (ModeParseException)
            {
                Console.WriteLine(Properties.Resources.ModeParseError);
                return 0;
            }
            catch (ArgsLengthException)
            {
                Console.WriteLine(Properties.Resources.IncorrectArgsLength);
                return 0;
            }
        }
    }
}
