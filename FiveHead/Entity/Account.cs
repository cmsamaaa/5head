﻿using FiveHead.Scripts.Libraries;
using FiveHead.Entity;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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

        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateAccount(Account account)
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
                sqlCmd.Parameters.AddWithValue("@username", account.Username);
                sqlCmd.Parameters.AddWithValue("@password", account.Password);
                sqlCmd.Parameters.AddWithValue("@encryptKey", account.EncryptKey);
                sqlCmd.Parameters.AddWithValue("@profileID", account.ProfileID);
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

        public List<Account> GetAllAccounts()
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

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToList<Account>();
            else
                return null;
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