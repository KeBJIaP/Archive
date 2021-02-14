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
        static void Main(string[] args)
        {

            var arch = new ArchiverUnityFactory().Create();
            Console.WriteLine(arch.Start());
        }
    }
}
