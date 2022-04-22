using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;

namespace FiveHead.Controller
{
    public class AccountsController
    {
        Encryption crypt = new Encryption();

        Account account = new Account();
        Profile profile = new Profile();

        ProfilesController profilesBLL = new ProfilesController();

        public int CreateAccount(string username, string password, int profileID)
        {
            if (!CheckUsernameExist(username))
            {
                string encryptKey, encryptPassword;

                encryptKey = RNGCrypto.GenerateIdentifier(12);
                encryptPassword = crypt.Encrypt(encryptKey, password);
                account = new Account(username, encryptPassword, encryptKey, profileID);

                return account.CreateAccount(account);
            }
            else
                return 0;
        }

        public int CreateAdminAccount(string sessionUser, string username, string password)
        {
            account = GetAccountByUsername(sessionUser);
            string profileName = profilesBLL.GetProfileNameByID(account.ProfileID);

            if (profileName.Equals("Administrator"))
                return CreateAccount(username, password, profilesBLL.GetProfileIDByName("Administrator"));
            else
                return 0;
        }

        public List<Account> GetAllAccounts()
        {
            return account.GetAllAccounts();
        }

        public Account GetAccountByUsername(string username)
        {
            return account.GetAccountByUsername(username);
        }

        public bool CheckUsernameExist(string username)
        {
            account = GetAccountByUsername(username);
            return account != null;
        }

        public int GetAccountIDByUsername(string username)
        {
            account = GetAccountByUsername(username);
            if (account != null)
                return account.AccountID;
            else
                return 0;
        }

        public Account GetAccount(string username, string password)
        {
            account = GetAccountByUsername(username);

            if(account == null)
                return null;

            string encryptKey = account.EncryptKey;
            string encryptPassword = crypt.Encrypt(encryptKey, password);

            return account.GetAccount(username, encryptPassword);
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

            return account.UpdatePassword(username, encryptPassword);
        }

        public int SuspendAccount(string username)
        {
            return account.SuspendAccount(username);
        }

        public int DeleteAccount(string username)
        {
            return account.DeleteAccount(username);
        }
    }
}