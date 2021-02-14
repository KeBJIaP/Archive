using System.Collections.Generic;

namespace GZipTest.Components.UserInteraction
{
    public class ConsoleUserInteractionsService : IUserInteractionsService
    {
        private readonly IEqualityComparer<string> _stringComparer;

        public ConsoleUserInteractionsService(IEqualityComparer<string> stringComparer)
        {
            _stringComparer = stringComparer ?? throw new System.ArgumentNullException(nameof(stringComparer));
        }

        public bool AskYesNo(string message)
        {
            System.Console.WriteLine($"{message} (y/n)");
            var input = System.Console.ReadKey().KeyChar.ToString();
            System.Console.WriteLine();
            if (_stringComparer.Equals(input, "y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
