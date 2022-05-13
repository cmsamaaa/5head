using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

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

        public Account(int accountID, string username) : this(username)
        {
            this.AccountID = accountID;
        }

        public Account(int accountID, bool deactivated)
        {
            this.AccountID = accountID;
            this.Deactivated = deactivated;
        }

        public Account(string username, string password, string encryptKey) : this(username)
        {
            this.Password = password;
            this.EncryptKey = encryptKey;
        }

        public Account(string username, string password, string encryptKey, int profileID) : this(username, password, encryptKey)
        {
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

        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateAccount()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Accounts (username, password, encryptKey, profileID)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@username, @password, @encryptKey, @profileID)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@username", this.Username);
                sqlCmd.Parameters.AddWithValue("@password", this.Password);
                sqlCmd.Parameters.AddWithValue("@encryptKey", this.EncryptKey);
                sqlCmd.Parameters.AddWithValue("@profileID", this.ProfileID);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public DataSet GetAllAccounts()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Accounts");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return ds;
        }

        public DataSet Admin_GetAllAccounts()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Accounts a");
            sql.AppendLine(" ");
            sql.AppendLine("INNER JOIN Profiles p");
            sql.AppendLine(" ");
            sql.AppendLine("ON a.profileID = p.profileID");
            sql.AppendLine(" ");
            sql.AppendLine("ORDER BY accountID ASC");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return ds;
        }

        public Account GetAccountByUsername(string username)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE username = @username");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("username", username);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            if (ds.Tables[0].Rows.Count > 0)
                return new Account(ds.Tables[0].ToList<Account>()[0]);
            else
                return null;
        }

        public Account GetAccount(int accountID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE accountID = @accountID");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("accountID", accountID);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            if (ds.Tables[0].Rows.Count > 0)
                return new Account(ds.Tables[0].ToList<Account>()[0]);
            else
                return null;
        }

        public Account GetAccount(string username, string password)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE username = @username AND password = @password");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("username", username);
                da.SelectCommand.Parameters.AddWithValue("password", password);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            if (ds.Tables[0].Rows.Count > 0)
                return new Account(ds.Tables[0].ToList<Account>()[0]);
            else
                return null;
        }

        public int UpdateUsername()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("SET username=@username");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE accountID=@accountID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@accountID", this.AccountID);
                sqlCmd.Parameters.AddWithValue("@username", this.Username);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public int UpdatePassword()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("SET password=@password, encryptKey=@encryptKey");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE username=@username");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@username", this.Username);
                sqlCmd.Parameters.AddWithValue("@password", this.Password);
                sqlCmd.Parameters.AddWithValue("@encryptKey", this.EncryptKey);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public int UpdateAccountStatus()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("SET deactivated=@deactivated");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE accountID=@accountID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@accountID", this.AccountID);
                sqlCmd.Parameters.AddWithValue("@deactivated", this.Deactivated);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }
    }
}