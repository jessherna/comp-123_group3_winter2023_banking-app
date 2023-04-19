using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    static class Bank
    {
        public static readonly Dictionary<string, Account> ACCOUNTS = new Dictionary<string, Account>()
        {
            { "VS-100000", new VisaAccount() },
            { "VS-100001", new VisaAccount(150, -500) },
            { "SV-100002", new SavingAccount(5000) },
            { "SV-100003", new SavingAccount() },
            { "CK-100004", new CheckingAccount(2000) },
            { "CK-100005", new CheckingAccount(1500, true) },
            { "VS-100006", new VisaAccount(50, -550) },
            { "SV-100007", new SavingAccount(1000) }
        };

        public static readonly Dictionary<string, Person> USERS = new Dictionary<string, Person>()
        {
            { "Narendra", new Person("Narendra", "1234-5678") },
            { "Ilia", new Person("Ilia", "2345-6789") },
            { "Mehrdad", new Person("Mehrdad", "3456-7890") },
            { "Vijay", new Person("Vijay", "4567-8901") },
            { "Arben", new Person("Arben", "5678-9012") },
            { "Patrick", new Person("Patrick", "6789-0123") },
            { "Yin", new Person("Yin", "7890-1234") },
            { "Hao", new Person("Hao", "8901-2345") },
            { "Jake", new Person("Jake", "9012-3456") },
            { "Mayy", new Person("Mayy", "1224-5678") },
            { "Nicoletta", new Person("Nicoletta", "2344-6789") }
        };

        static Bank()
        {
            //associate users with accounts
            string number = "VS-100000";
            AddUserToAccount(number, "Narendra");
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Mehrdad");

            number = "VS-100001";
            AddUserToAccount(number, "Vijay");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Patrick");

            number = "SV-100002";
            AddUserToAccount(number, "Yin");
            AddUserToAccount(number, "Hao");
            AddUserToAccount(number, "Jake");

            number = "SV-100003";
            AddUserToAccount(number, "Mayy");
            AddUserToAccount(number, "Nicoletta");

            number = "CK-100004";
            AddUserToAccount(number, "Mehrdad");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Yin");

            number = "CK-100005";
            AddUserToAccount(number, "Jake");
            AddUserToAccount(number, "Nicoletta");

            number = "VS-100006";
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Vijay");

            number = "SV-100007";
            AddUserToAccount(number, "Patrick");
            AddUserToAccount(number, "Hao");
        }

        public static void PrintAccounts()
        {
            foreach (Account account in ACCOUNTS.Values)
            {
                string result = $"[{account.Number}, {account.Number} ";
                for (int i = 0; i < account.users.Count; i++)
                {
                    result += $"{account.users[i]}";

                    if (i < account.users.Count - 1)
                    {
                        result += ", ";
                    }
                }
                result += $" {account.Balance:C2} ";
                result += $"- transactions ({account.transactions.Count})]";
                Console.WriteLine(result);
            }

        }

        public static void PrintPersons()
        {
            foreach (KeyValuePair<string, Person> person in USERS)
            {
                string status = person.Value.IsAuthenticated ? "Logged in" : "not logged in";
                Console.WriteLine($"[{person.Value.Name}, {person.Value.Name} {status}]");
            }
        }

        public static Person GetPerson(string name)
        {
            if (USERS.ContainsKey(name))
            {
                return USERS[name];
            }
            else
            {
                throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
            }
        }

        public static Account GetAccount(string number)
        {
            if (ACCOUNTS.ContainsKey(number))
            {
                return ACCOUNTS[number];
            }
            else
            {
                throw new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
            }
        }
        
        public static List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach (Account account in ACCOUNTS.Values)
            {
                transactions.AddRange(account.transactions);
            }

            return transactions;
        }

        public static void AddPerson(string name, string sin)
        {
            Person person = new Person(name, sin);
            person.OnLogin += Logger.LoginHandler;
            USERS.Add(name, person);
        }

        public static void AddAccount(Account account)
        {
            ACCOUNTS.Add(account.Number, account);
        }

        public static void AddUserToAccount(string number, string name)
        {
            var account = GetAccount(number);
            var person = GetPerson(name);
            account.AddUser(person);
        }

    }
}
