using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    abstract class Account
    {
        private static int LAST_NUMBER = 100000;

        public string Number { get; }
        public double Balance { get; protected set; }
        public double LowestBalance { get; protected set; }
        public List<Transaction> transactions { get; private set; }
        public List<Person> users { get; private set; }
        public event EventHandler<EventArgs> OnTransaction;

        public Account(string type, double balance)
        {
            Number = type + "-" + LAST_NUMBER;
            LAST_NUMBER++;
            Balance = balance;
            LowestBalance = balance;
            transactions = new List<Transaction>();
            users = new List<Person>();
        }

        public void Deposit(double amount, Person person)
        {
            Balance += amount;
            if (Balance < LowestBalance)
            {
                LowestBalance = Balance;
            }
            Transaction transaction = new Transaction(Number, amount, person);
            transactions.Add(transaction);
            

        }

        public void AddUser(Person person)
        {
            users.Add(person);
        }

        public bool IsUser(string name)
        {

            return users.Any(holder => holder.Name == name);
        }

        public abstract void PrepareMonthlyReport();

        public virtual void OnTransactionOccur(object sender, EventArgs args)
        {
            OnTransaction?.Invoke(sender, args);
        }

        public override string ToString()
        {
            string balanceString = Balance >= 0 ? $"{Balance:C2}" : $"-{(-Balance):C2}";
            string result = $"{Number} ";
            for (int i = 0; i < users.Count; i++)
            {
                result += $"{users[i]}";

                if (i < users.Count - 1)
                {
                    result += ", ";
                }
            }
            result += $" {balanceString} ";
            result += $"- transactions ({transactions.Count})\n";
            foreach (Transaction transaction in transactions)
            {
                result += $"    {transaction}\n";
            }
            return result;
        }

    }

}
