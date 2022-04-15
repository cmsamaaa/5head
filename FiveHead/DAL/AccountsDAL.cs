using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace FiveHead.DAL
{
    public class AccountsDAL
    {
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateAccount(string username, string password, string encryptID)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Accounts (username, password, encryptID)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@username, @password, @encryptID)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
                sqlCmd.Parameters.AddWithValue("@encryptID", encryptID);
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
            DataSet accountData;

            MySqlConnection conn = dbConn.GetConnection();
            accountData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Accounts");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.Fill(accountData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return accountData;
        }

        public DataSet GetAccountByUsername(string username)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet accountData;

            MySqlConnection conn = dbConn.GetConnection();
            accountData = new DataSet();
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
                da.Fill(accountData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return accountData;
        }

        public DataSet GetAccount(string username, string password)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet accountData;

            MySqlConnection conn = dbConn.GetConnection();
            accountData = new DataSet();
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
                da.Fill(accountData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return accountData;
        }

        public int UpdatePassword(string username, string password)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("SET password=@password");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE username=@username");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@password", password);
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

        public int SuspendAccount(string username)
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
            sql.AppendLine("WHERE username=@username");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@username", username);
                sqlCmd.Parameters.AddWithValue("@deactivated", true);
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

        public int DeleteAccount(string username)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Accounts");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE username=@username");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@username", username);
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