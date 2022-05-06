using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace FiveHead.Entity
{
    public class Category
    {
        private int categoryID;
        private string categoryName;
        private bool deactivated;

        public Category()
        {

        }

        public Category(string categoryName)
        {
            this.CategoryName = categoryName;
        }

        public Category(string categoryName, bool deactivated) : this(categoryName)
        {
            this.Deactivated = deactivated;
        }

        public Category(int categoryID, string categoryName, bool deactivated) : this(categoryName, deactivated)
        {
            this.CategoryID = categoryID;
        }

        public Category(Category category) : this(category.CategoryID, category.CategoryName, category.Deactivated)
        {
            if (category == null)
                throw new ArgumentNullException();
        }

        public int CategoryID { get => categoryID; set => categoryID = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }

        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateCategory(string categoryName)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Categories (categoryName)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@categoryName)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@categoryName", categoryName);
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

        public DataSet GetAllCategories()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Categories");
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

        public Category GetCategoryByID(int categoryID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Categories");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE categoryID = @categoryID");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("categoryID", categoryID);
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
                return new Category(ds.Tables[0].ToList<Category>()[0]);
            else
                return null;
        }

        public int UpdateCategoryName(int categoryID, string categoryName)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Categories");
            sql.AppendLine(" ");
            sql.AppendLine("SET categoryName=@categoryName");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE categoryID=@categoryID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@categoryID", categoryID);
                sqlCmd.Parameters.AddWithValue("@categoryName", categoryName);
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

        public int UpdateCategoryStatus(int categoryID, bool deactivated)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Categories");
            sql.AppendLine(" ");
            sql.AppendLine("SET deactivated=@deactivated");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE categoryID=@categoryID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@categoryID", categoryID);
                sqlCmd.Parameters.AddWithValue("@deactivated", deactivated);
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