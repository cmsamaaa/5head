using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiveHead.Entity
{
    public class Account
    {
        private int accountID;
        private string username;
        private string password;
        private string encryptID;
        private bool deactivated;

        public Account()
        {

        }

        public Account(string username)
        {
            this.Username = username;
        }

        public Account(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public Account(string username, string password, string encryptID)
        {
            this.Username = username;
            this.Password = password;
            this.EncryptID = encryptID;
        }

        public Account(int accountID, string username, string password, string encryptID, bool deactivated)
        {
            this.AccountID = accountID;
            this.Username = username;
            this.Password = password;
            this.EncryptID = encryptID;
            this.Deactivated = deactivated;
        }

        public Account(Account account)
        {
            if (account == null)
                throw new ArgumentNullException();

            this.AccountID = account.accountID;
            this.Username = account.username;
            this.Password = account.password;
            this.EncryptID = account.encryptID;
            this.Deactivated = account.deactivated;
        }

        public int AccountID { get => accountID; set => accountID = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string EncryptID { get => encryptID; set => encryptID = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }
    }
}