using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    class SavingAccount : Account, ITransaction
    {
        public static readonly double COST_PER_TRANSACTION = 0.05;
        public static readonly double INTEREST_RATE = 0.015;
        private Person previousPerson;
        private double previousAmount;

        public SavingAccount(double balance = 0) : base(Utils.ACCOUNT_TYPES[AccountType.Saving], balance)
        {

        }

        public new void Deposit(double amount, Person person)
        {
            base.Deposit(amount, person);
            OnTransaction -= Logger.TransactionHandler;
            OnTransaction += Logger.TransactionHandler;
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            
        }

        public void Withdraw(double amount, Person person)
        {
            if (!users.Contains(person))
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }
            if (!person.IsAuthenticated)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
            }
            if (amount > Balance)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.NO_OVERDRAFT);
            }
            base.Deposit(-amount, person);
            OnTransaction -= Logger.TransactionHandler;
            OnTransaction += Logger.TransactionHandler;
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }

        public override void PrepareMonthlyReport()
        {
            double serviceCharge = transactions.Count * COST_PER_TRANSACTION;
            double interest = LowestBalance * INTEREST_RATE;
            Balance = (Balance + interest) - serviceCharge;
            transactions.Clear();
        }
    }
}
