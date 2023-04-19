using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    class VisaAccount : Account
    {
        private double creditLimit;

        private const double INTEREST_RATE = 0.1995;

        public VisaAccount(double balance = 0, double creditLimit = 1200) 
            : base(Utils.ACCOUNT_TYPES[AccountType.Visa], balance)
        {
            this.creditLimit = creditLimit;
        }

        public void DoPayment(double amount, Person person)
        {
            base.Deposit(amount, person);
            OnTransaction -= Logger.TransactionHandler;
            OnTransaction += Logger.TransactionHandler;
            base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
           
        }

        public void DoPurchase(double amount, Person person)
        {
            if (!IsUser(person.Name))
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            if (!person.IsAuthenticated)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
            }

            if (amount > Balance + creditLimit)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
            }

            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            base.Deposit(-amount, person);
        }

        public override void PrepareMonthlyReport()
        {
            double interest = LowestBalance * INTEREST_RATE;
            Balance += interest;
            transactions.Clear();
        }
    }
}
