using FiveHead.DAL;
using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;

namespace FiveHead.BLL
{
    public class AccountsBLL
    {
        Account account;
        Profile profile;
        AccountsDAL dataLayer = new AccountsDAL();
        ProfilesBLL profilesBLL = new ProfilesBLL();
        Encryption crypt = new Encryption();

        public int CreateAccount(string username, string password)
        {
            string encryptKey, encryptPassword;

            encryptKey = RNGCrypto.GenerateIdentifier(12);
            encryptPassword = crypt.Encrypt(encryptKey, password);
            account = new Account(username, encryptPassword, encryptKey);

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
            account = GetAccountByUsername(username);

            if(account == null)
                return null;

            string encryptKey = account.EncryptKey;
            string encryptPassword = crypt.Encrypt(encryptKey, password);

            return dataLayer.GetAccount(username, encryptPassword);
        }

        public bool Authenticate(string username, string password)
        {
            return GetAccount(username, password) == null ? false : true;
        }

        public bool Admin_Authentication(string username, string password)
        {
            account = GetAccount(username, password);
            profile = profilesBLL.GetProfileByID(account.ProfileID);

            if (profile.ProfileName.Equals("Administrator"))
                return true;

            return false;
        }

        public int UpdatePassword(string username, string password, string encryptKey)
        {
            string encryptPassword = crypt.Encrypt(encryptKey, password);

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