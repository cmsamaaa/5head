using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FiveHead.Entity
{
    public class Staff
    {
        private int staffID;
        private string firstName;
        private string lastName;
        private int accountID;

        public Staff()
        {

        }

        public Staff(int staffID)
        {
            this.StaffID = staffID;
        }

        public Staff(string firstName, string lastName, int accountID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AccountID = accountID;
        }

        public Staff(int staffID, string firstName, string lastName) : this(staffID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Staff(int staffID, string firstName, string lastName, int accountID) : this(staffID, firstName, lastName)
        {
            this.AccountID = accountID;
        }

        public Staff(int staffID, string firstName, string lastName, double salary, int accountID) : this(staffID, firstName, lastName, accountID)
        {
            this.AccountID = accountID;
        }

        public Staff(Staff staff) : this(staff.StaffID, staff.firstName, staff.LastName, staff.AccountID)
        {
            if (staff == null)
                throw new ArgumentNullException();
        }

        public int StaffID { get => staffID; set => staffID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int AccountID { get => accountID; set => accountID = value; }

        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateStaff()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Staffs (firstName, lastName, accountID)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@firstName, @lastName, @accountID)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@firstName", this.FirstName);
                sqlCmd.Parameters.AddWithValue("@lastName", this.LastName);
                sqlCmd.Parameters.AddWithValue("@accountID", this.AccountID);
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

        public List<Staff> GetAllStaff()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Staffs");
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
                return ds.Tables[0].ToList<Staff>();
            else
                return null;
        }

        public Staff GetStaffByStaffID(int staffID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Staffs");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE staffID = @staffID");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("staffID", staffID);
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
                return new Staff(ds.Tables[0].ToList<Staff>()[0]);
            else
                return null;
        }

        public Staff GetStaffByAccountID(int accountID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Staffs");
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
                return new Staff(ds.Tables[0].ToList<Staff>()[0]);
            else
                return null;
        }

        public int UpdateName()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Staffs");
            sql.AppendLine(" ");
            sql.AppendLine("SET firstName=@firstName, lastName=@lastName");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE staffID=@staffID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@staffID", this.StaffID);
                sqlCmd.Parameters.AddWithValue("@firstName", this.FirstName);
                sqlCmd.Parameters.AddWithValue("@lastName", this.LastName);
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

        public int DeleteStaff(int staffID)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Staffs");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE staffID=@staffID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("staffID", staffID);
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