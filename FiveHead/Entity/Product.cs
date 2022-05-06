using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace FiveHead.Entity
{
    public class Product
    {
        private int productID;
        private string productName;
        private double price;
        private bool deactivated;
        private int categoryID;

        public Product()
        {

        }

        public Product(string productName)
        {
            this.ProductName = productName;
        }

        public Product(string productName, double price) : this(productName)
        {
            this.Price = price;
        }

        public Product(string productName, double price, int categoryID) : this(productName, price)
        {
            this.CategoryID = categoryID;
        }

        public Product(int productID, string productName, double price, int categoryID) : this(productName, price, categoryID)
        {
            this.ProductID = productID;
        }

        public Product(int productID, string productName, double price, bool deactivated, int categoryID) : this(productID, productName, price, categoryID)
        {
            this.Deactivated = deactivated;
        }

        public Product(Product product) : this(product.ProductID, product.ProductName, product.Price, product.Deactivated, product.CategoryID)
        {
            if (product == null)
                throw new ArgumentNullException();
        }

        public int ProductID { get => productID; set => productID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public double Price { get => price; set => price = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }

        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateProduct(Product product)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Products (productName, price, categoryID)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@productName, @price, @categoryID)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@productName", product.ProductName);
                sqlCmd.Parameters.AddWithValue("@price", product.Price);
                sqlCmd.Parameters.AddWithValue("@categoryID", product.CategoryID);
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

        public DataSet GetAllProducts()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Products");
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

        public Product GetProductByID(int productID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Products");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE productID = @productID");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("productID", productID);
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
                return new Product(ds.Tables[0].ToList<Product>()[0]);
            else
                return null;
        }

        public int UpdateProduct(Product product)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Products");
            sql.AppendLine(" ");
            sql.AppendLine("SET productName=@productName, price=@price, categoryID=@categoryID");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE productID=@productID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@productID", product.ProductID);
                sqlCmd.Parameters.AddWithValue("@productName", product.ProductName);
                sqlCmd.Parameters.AddWithValue("@price", product.price);
                sqlCmd.Parameters.AddWithValue("@categoryID", product.categoryID);
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

        public int UpdateProductStatus(int productID, bool deactivated)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Products");
            sql.AppendLine(" ");
            sql.AppendLine("SET deactivated=@deactivated");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE productID=@productID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@productID", productID);
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