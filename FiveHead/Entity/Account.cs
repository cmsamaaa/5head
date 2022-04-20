using System;

namespace FiveHead.Entity
{
    public class Account
    {
        private int accountID;
        private string username;
        private string password;
        private string encryptKey;
        private int profileID;
        private bool deactivated;

        public Account()
        {

        }

        public Account(string username)
        {
            this.Username = username;
        }

        public Account(string username, string password) : this(username)
        {
            this.Password = password;
        }

        public Account(string username, string password, string encryptKey, int profileID) : this(username, password)
        {
            this.EncryptKey = encryptKey;
            this.ProfileID = profileID;
        }

        public Account(int accountID, string username, string password, string encryptKey, int profileID, bool deactivated) : this (username, password, encryptKey, profileID)
        {
            this.AccountID = accountID;
            this.profileID = profileID;
            this.Deactivated = deactivated;
        }

        public Account(Account account) : this(account.AccountID, account.Username, account.Password, account.EncryptKey, account.ProfileID, account.Deactivated)
        {
            if (account == null)
                throw new ArgumentNullException();
        }

        public int AccountID { get => accountID; set => accountID = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string EncryptKey { get => encryptKey; set => encryptKey = value; }
        public int ProfileID { get => profileID; set => profileID = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }
    }
}