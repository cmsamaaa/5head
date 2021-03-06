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
        private byte[] image;
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

        public Product(int productID, bool deactivated)
        {
            this.ProductID = productID;
            this.Deactivated = deactivated;
        }

        public Product(string productName, double price) : this(productName)
        {
            this.Price = price;
        }

        public Product(string productName, byte[] image, double price, int categoryID) : this(productName, price)
        {
            this.Image = image;
            this.CategoryID = categoryID;
        }

        public Product(int productID, string productName, byte[] image, double price, int categoryID) : this(productName, image, price, categoryID)
        {
            this.ProductID = productID;
        }

        public Product(int productID, string productName, byte[] image, double price, bool deactivated, int categoryID) : this(productID, productName, image, price, categoryID)
        {
            this.Deactivated = deactivated;
        }

        public Product(Product product) : this(product.ProductID, product.ProductName, product.Image, product.Price, product.Deactivated, product.CategoryID)
        {
            if (product == null)
                throw new ArgumentNullException();
        }

        public int ProductID { get => productID; set => productID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public byte[] Image { get => image; set => image = value; }
        public double Price { get => price; set => price = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }

        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateProduct()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Products (productName, image, price, categoryID)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@productName, @image, @price, @categoryID)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@productName", this.ProductName);
                sqlCmd.Parameters.AddWithValue("@image", this.Image);
                sqlCmd.Parameters.AddWithValue("@price", this.Price);
                sqlCmd.Parameters.AddWithValue("@categoryID", this.CategoryID);
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

        public DataSet GetAllActiveProducts()
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
            sql.AppendLine("WHERE deactivated=@deactivated");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("deactivated", false);
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

        public DataSet GetAllActiveProductByCategoryID(int categoryID)
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
            sql.AppendLine("WHERE categoryID=@categoryID");
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

            return ds;
        }

        public DataSet SearchAllProducts(string search)
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
            sql.AppendLine("WHERE productName LIKE @search");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("search", "%" + search + "%");
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

        public DataSet SearchAllActiveProducts(string search)
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
            sql.AppendLine("WHERE productName LIKE @search AND deactivated=@deactivated");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("search", "%" + search + "%");
                da.SelectCommand.Parameters.AddWithValue("deactivated", false);
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

        public int UpdateProduct()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Products");
            sql.AppendLine(" ");
            sql.AppendLine("SET productName=@productName, image=@image, price=@price, categoryID=@categoryID");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE productID=@productID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@productID", this.ProductID);
                sqlCmd.Parameters.AddWithValue("@productName", this.ProductName);
                sqlCmd.Parameters.AddWithValue("@image", this.Image);
                sqlCmd.Parameters.AddWithValue("@price", this.price);
                sqlCmd.Parameters.AddWithValue("@categoryID", this.categoryID);
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

        public int UpdateProductStatus()
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
                sqlCmd.Parameters.AddWithValue("@productID", this.ProductID);
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