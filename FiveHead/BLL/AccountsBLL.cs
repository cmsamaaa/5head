using FiveHead.DAL;
using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;
using System.Data;

namespace FiveHead.BLL
{
    public class AccountsBLL
    {
        Account account;
        AccountsDAL dataLayer = new AccountsDAL();
        Encryption crypt = new Encryption();

        public int CreateAccount(string username, string password)
        {
            string encryptID, encryptPassword;

            encryptID = RNGCrypto.GenerateIdentifier(12);
            encryptPassword = crypt.Encrypt(encryptID, password);
            account = new Account(username, encryptPassword, encryptID);

            return dataLayer.CreateAccount(account);
        }

        public List<Account> GetAllAccounts()
        {
            return dataLayer.GetAllAccounts();
        }

        public Account GetAccountByUsername(string username)
        {
            return dataLayer.GetAccountByUsername(username);
        }

        public Account GetAccount(string username, string password)
        {
            account = new Account(GetAccountByUsername(username));
            string encryptID = account.EncryptID;
            string encryptPassword = crypt.Encrypt(encryptID, password);

            return dataLayer.GetAccount(username, encryptPassword);
        }

        public bool Authenticate(string username, string password)
        {
            return GetAccount(username, password) == null ? false : true;
        }

        public int UpdatePassword(string username, string password, string encryptID)
        {
            string encryptPassword = crypt.Encrypt(encryptID, password);

            return dataLayer.UpdatePassword(username, encryptPassword);
        }

        public int SuspendAccount(string username)
        {
            return dataLayer.SuspendAccount(username);
        }

        public int DeleteAccount(string username)
        {
            return dataLayer.DeleteAccount(username);
        }
    }
}