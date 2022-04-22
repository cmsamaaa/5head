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
        private int permissionLevel;

        public Profile()
        {

        }

        public Profile(string profileName, int permissionLevel)
        {
            this.ProfileName = profileName;
            this.PermissionLevel = permissionLevel;
        }

        public Profile(int profileID, string profileName, int permissionLevel) : this(profileName, permissionLevel)
        {
            this.ProfileID = profileID;
        }

        public Profile (Profile profile) : this(profile.ProfileID, profile.ProfileName, profile.PermissionLevel)
        {
            if (profile == null)
                throw new ArgumentNullException();
        }

        public int ProfileID { get => profileID; set => profileID = value; }
        public string ProfileName { get => profileName; set => profileName = value; }
        public int PermissionLevel { get => permissionLevel; set => permissionLevel = value; }


        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateProfile(Profile profile)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Profiles (profileName, permissionLevel)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@profileName, @permissionLevel)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@profileName", profile.ProfileName);
                sqlCmd.Parameters.AddWithValue("@permissionLevel", profile.PermissionLevel);
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

        public List<Profile> GetAllProfiles()
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
                return ds.Tables[0].ToList<Profile>();
            else
                return null;
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
    }
}