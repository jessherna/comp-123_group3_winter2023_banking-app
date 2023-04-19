using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    class Person
    {
        private string password;
        public event EventHandler OnLogin;

        public string SIN { get; }
        public string Name { get; }
        public bool IsAuthenticated { get; private set; }

        public Person(string name, string sin)
        {
            Name = name;
            SIN = sin;
            password = sin.Substring(0, 3);
        }

        public void Login(string password)
        {
            if (password != this.password)
            {
                IsAuthenticated = false;
                OnLogin?.Invoke(this, new LoginEventArgs(Name, false));
                throw new AccountException(ExceptionType.PASSWORD_INCORRECT);
            }
            else
            {
                IsAuthenticated = true;
                OnLogin += Logger.LoginHandler;
                OnLogin?.Invoke(this, new LoginEventArgs(Name, true));
            }
            
        }

        public void Logout()
        {
            IsAuthenticated = false;
        }

        public override string ToString()
        {
            return $"{Name} {(IsAuthenticated ? "Logged in" : "Not logged in")}";
        }
    }
}
