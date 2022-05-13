using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FiveHead.Entity
{
    public class Profile
    {
        private int profileID;
        private string profileName;
        private bool deactivated;

        public Profile()
        {

        }

        public Profile(string profileName)
        {
            this.ProfileName = profileName;
        }

        public Profile(int profileID, string profileName)
        {
            this.ProfileID = profileID;
            this.ProfileName = profileName;
        }

        public Profile(int profileID, bool deactivated)
        {
            this.ProfileID = profileID;
            this.Deactivated = deactivated;
        }

        public Profile(int profileID, string profileName, bool deactivated) : this(profileID, profileName)
        {
            this.Deactivated = deactivated;
        }

        public Profile (Profile profile) : this(profile.ProfileID, profile.ProfileName, profile.Deactivated)
        {
            if (profile == null)
                throw new ArgumentNullException();
        }

        public int ProfileID { get => profileID; set => profileID = value; }
        public string ProfileName { get => profileName; set => profileName = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }


        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateProfile()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Profiles (profileName)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@profileName)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@profileName", this.ProfileName);
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

        public DataSet GetAllProfiles()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Profiles");
            sql.AppendLine(" ");
            sql.AppendLine("ORDER BY profileID ASC");
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

        public Profile GetProfileByID(int profileID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Profiles");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE profileID = @profileID");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("profileID", profileID);
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
                return new Profile(ds.Tables[0].ToList<Profile>()[0]);
            else
                return null;
        }

        public Profile GetProfileByName(string profileName)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Profiles");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE profileName = @profileName");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("profileName", profileName);
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
                return new Profile(ds.Tables[0].ToList<Profile>()[0]);
            else
                return null;
        }

        public int UpdateProfileName()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Profiles");
            sql.AppendLine(" ");
            sql.AppendLine("SET profileName=@profileName");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE profileID=@profileID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@profileID", this.ProfileID);
                sqlCmd.Parameters.AddWithValue("@profileName", this.ProfileName);
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

        public int UpdateProfileStatus()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Profiles");
            sql.AppendLine(" ");
            sql.AppendLine("SET deactivated=@deactivated");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE profileID=@profileID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@profileID", this.ProfileID);
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