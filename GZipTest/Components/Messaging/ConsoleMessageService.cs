using System;

namespace GZipTest.Components.Messaging
{
    public class ConsoleMessageService : IMessagesService
    {
        public void Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}
