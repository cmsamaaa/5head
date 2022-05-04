using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;
using System.Data;

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

        public DataSet GetAllAccountsDataSet()
        {
            return account.GetAllAccounts();
        }

        public DataSet Admin_GetAllAccounts()
        {
            return account.Admin_GetAllAccounts();
        }

        public List<Account> GetAllAccounts()
        {
            DataSet ds = GetAllAccountsDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToList<Account>();
            else
                return null;
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

        public Account GetAccount(int accountID)
        {
            return account.GetAccount(accountID);
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
            account = GetAccount(username, password);
            if (account.Deactivated)
                return false;
            else
                return true;
        }

        public bool Admin_Authentication(string username, string password)
        {
            account = GetAccount(username, password);
            if (account == null || account.Deactivated)
                return false;

            profile = profilesBLL.GetProfileByID(account.ProfileID);
            if (profile.ProfileName.Equals("Administrator"))
                return true;

            return false;
        }

        public int UpdateUsername(int accountID, string username)
        {
            if (!CheckUsernameExist(username))
            {
                account = new Account();
                return account.UpdateUsername(accountID, username);
            }
            else
                return 0;
        }

        public int UpdatePassword(string username, string password)
        {
            string encryptKey, encryptPassword;
            encryptKey = RNGCrypto.GenerateIdentifier(12);
            encryptPassword = crypt.Encrypt(encryptKey, password);
            account = new Account(username, encryptPassword, encryptKey);

            return account.UpdatePassword(username, encryptPassword, encryptKey);
        }

        public int ReactivateAccount(int accountID)
        {
            return account.UpdateAccountStatus(accountID, false);
        }

        public int SuspendAccount(int accountID)
        {
            return account.UpdateAccountStatus(accountID, true);
        }
    }
}