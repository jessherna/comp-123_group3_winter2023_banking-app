using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    struct Transaction
    {
        public string AccountNumber { get; }
        public double Amount { get; }
        public Person Originator { get; }
        public DayTime Time { get; }

        public Transaction(string accountNumber, double amount, Person person)
            : this(accountNumber, amount, person, Utils.Time)
        {
        }

        public Transaction(string accountNumber, double amount, Person originator, DayTime time)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            Originator = originator;
            Time = time;

        }

        public override string ToString()
        {
            string amountString = Amount >= 0 ? $"{Amount:C2}" : $"{(-Amount):C2}";
            string depositOrWithdrawal = Amount >= 0 ? "deposited" : "withdrawn";
            return $"{AccountNumber} {amountString} {depositOrWithdrawal} by {Originator.Name} on {Time}";
        }

    }
}
