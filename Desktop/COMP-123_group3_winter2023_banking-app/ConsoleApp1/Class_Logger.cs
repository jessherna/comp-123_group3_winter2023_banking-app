using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    class Logger
    {
        private static List<string> loginEvents = new List<string>();
        private static List<string> transactionEvents = new List<string>();

        public static void LoginHandler(object sender, EventArgs args)
        {
            var loginArgs = args as LoginEventArgs;
            string _success = loginArgs.Success ? "successfully" : "unsuccessfully";
            string log = $"{loginArgs.PersonName} logged in {_success} on {Utils.Now}";
            loginEvents.Add(log);
        }

        public static void TransactionHandler(object sender, EventArgs args)
        {
            
            var transactionArgs = args as TransactionEventArgs;
            string _success = transactionArgs.Success ? "successfully" : "unsuccessfully";
            string log = $"{transactionArgs.PersonName} deposit {transactionArgs.Amount:C2} {_success} on {Utils.Now}";
            transactionEvents.Add(log);
        }

        public static void ShowLoginEvents()
        {
            Console.WriteLine($"Login events as of {Utils.Now}");
            for (int i = 0; i < loginEvents.Count; i++)
            {
                Console.WriteLine($"{i + 1} {loginEvents[i]}");
            }
        }

        public static void ShowTransactionEvents()
        {
            Console.WriteLine($"Transaction events as of {Utils.Now}");
            for (int i = 0; i < transactionEvents.Count; i++)
            {
                Console.WriteLine($"{i + 1} {transactionEvents[i]}");
            }
        }
    }

}
