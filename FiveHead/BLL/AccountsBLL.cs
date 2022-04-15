using FiveHead.DAL;
using FiveHead.Scripts.Libraries;
using System;
using System.Data;

namespace FiveHead.BLL
{
    public class AccountsBLL
    {
        AccountsDAL dataLayer = new AccountsDAL();
        Encryption crypt = new Encryption();

        public int CreateAccount(string username, string password)
        {
            string encryptID, encryptPassword;

            encryptID = RNGCrypto.GenerateIdentifier(12);
            encryptPassword = crypt.Encrypt(encryptID, password);

            return dataLayer.CreateAccount(username, encryptPassword, encryptID);
        }

        public DataSet GetAllAccounts()
        {
            return dataLayer.GetAllAccounts();
        }

        public DataSet GetAccountByUsername(string username)
        {
            return dataLayer.GetAccountByUsername(username);
        }

        public DataSet GetAccount(string username, string password)
        {
            string encryptID = "";
            DataSet ds = GetAccountByUsername(username);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                encryptID = row["encryptID"].ToString();

            }

            string encryptPassword = crypt.Encrypt(encryptID, password);

            return dataLayer.GetAccount(username, encryptPassword);
        }

        public bool Authenticate(string username, string password)
        {
            DataSet ds;
            DataTable dt;
            bool verify;

            verify = false;

            ds = GetAccount(username, password);
            dt = ds.Tables[0];

            verify = dt.Rows.Count == 1 ? true : false;

            return verify;
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