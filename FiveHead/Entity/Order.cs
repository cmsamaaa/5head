using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FiveHead.Entity
{
    public class Order
    {
        private int orderID;
        private int tableNumber;
        private int productID;
        private int categoryID;
        private string productName;
        private int productQty;
        private double price;
        private DateTime start_datetime;
        private DateTime end_datetime;
        private string paymentStatus;
        private string orderStatus;
        private double finalPrice;
        private string couponCode;
        private string contacts;

        /*
         * Entity Objects
         */
        public Order()
        {
            /* Constructor */
        }

        public Order(int orderID)
        {
            this.OrderID = orderID;
        }

        public Order(int tableNumber, string orderStatus, DateTime end_datetime)
        {
            this.TableNumber = tableNumber;
            this.OrderStatus = orderStatus;
            this.End_datetime = end_datetime;
        }

        public Order(int tableNumber, string paymentStatus, string orderStatus, DateTime end_datetime): this(tableNumber, orderStatus, end_datetime)
        {
            this.PaymentStatus = paymentStatus;
        }

        public Order(int tableNumber, int productID, int categoryID, string productName, int productQty, double price, DateTime start_datetime, DateTime end_datetime, double finalPrice, string couponCode, string contacts)
        {
            this.TableNumber = tableNumber;
            this.ProductID = productID;
            this.CategoryID = categoryID;
            this.ProductName = productName;
            this.ProductQty = productQty;
            this.Price = price;
            this.Start_datetime = start_datetime;
            this.End_datetime = end_datetime;
            this.FinalPrice = finalPrice;
            this.CouponCode = couponCode;
            this.Contacts = contacts;
        }

        public Order(Order order) : this(order.OrderID)
        {
            if(order == null)
            {
                throw new ArgumentNullException();
            }
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public int TableNumber { get => tableNumber; set => tableNumber = value; }
        public int ProductID { get => productID; set => productID = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int ProductQty { get => productQty; set => productQty = value; }
        public double Price { get => price; set => price = value; }
        public DateTime Start_datetime { get => start_datetime; set => start_datetime = value; }
        public DateTime End_datetime { get => end_datetime; set => end_datetime = value; }
        public string PaymentStatus { get => paymentStatus; set => paymentStatus = value; }
        public string OrderStatus { get => orderStatus; set => orderStatus = value; }
        public double FinalPrice { get => finalPrice; set => finalPrice = value; }
        public string CouponCode { get => couponCode; set => couponCode = value; }
        public string Contacts { get => contacts; set => contacts = value; }

        /*
         * Class Variables
         */
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        /*
         * Function Definitions
         */

        // Order Functions

        public int CreateOrder()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Orders (tableNumber, productID, categoryID, productName, productQty, price, start_datetime, end_datetime, finalPrice, couponCode, contacts)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@tableNumber, @productID, @categoryID, @productName, @productQty, @price, @start_datetime, @end_datetime, @finalPrice, @couponCode, @contacts)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@tableNumber", this.TableNumber);
                sqlCmd.Parameters.AddWithValue("@productID", this.ProductID);
                sqlCmd.Parameters.AddWithValue("@categoryID", this.CategoryID);
                sqlCmd.Parameters.AddWithValue("@productName", this.ProductName);
                sqlCmd.Parameters.AddWithValue("@productQty", this.ProductQty);
                sqlCmd.Parameters.AddWithValue("@price", this.Price);
                sqlCmd.Parameters.AddWithValue("@start_datetime", this.start_datetime);
                sqlCmd.Parameters.AddWithValue("@end_datetime", this.end_datetime);
                sqlCmd.Parameters.AddWithValue("@finalPrice", this.finalPrice);
                sqlCmd.Parameters.AddWithValue("@couponCode", this.couponCode);
                sqlCmd.Parameters.AddWithValue("@contacts", this.contacts);
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

        public int insert_Orders(
            int orderID, int tableNumber, int productID, int categoryID, 
            string productName, int productQty, double price, string start_datetime, 
            string end_datetime, string paymentStatus, string orderStatus, double finalPrice, string contacts
        )
        {
            /*
             * Insert Orders into Table
             */

            // Variables
            string sql_stmt = "INSERT INTO orders (" +
                "tableNumber, " +
                "productID, " +
                "categoryID, " +
                "productName, " +
                "productQty, " +
                "price, " +
                "start_datetime, " +
                "end_datetime, " +
                "paymentStatus, " +
                "orderStatus, " + 
                "finalPrice, " +
                "contacts" +
            ") " + "VALUES (" +
                tableNumber + ", " +
                productID + ", " +
                categoryID + ", " +
                "'" + productName + "'" + ", " + 
                productQty + ", " + 
                price + ", " + 
                "'" + start_datetime + "'" + ", " + 
                "'" + end_datetime + "'" + ", " + 
                "'" + paymentStatus + "'" + ", " +
                "'" + orderStatus + "'" + ", " +
                finalPrice + ", "  + 
                "'" + contacts + "'" +
            ");";
            int result = 0; // Return 1 => Success | 0 => Error
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand sql_cmd;

            // Execute SQL
            try
            {
                conn.Open();
                sql_cmd = mySQL.cmd_set_connection(sql_stmt, conn);
                result = sql_cmd.ExecuteNonQuery(); // Returns number of rows affected
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }
            System.Diagnostics.Debug.WriteLine(sql_stmt);

            return result;
        }

        public int update_Orders(int table_Num, string orderStatus, string contactDetails, string end_datetime)
        {
            /*
             * Update Order Status after payment is made		
             */

            // Default
            if (contactDetails == "")
            {
                contactDetails = "Not Provided";
            }

            // Variables
            int result = 0;
            string sql_stmt = "UPDATE orders SET" + " " + 
                "paymentStatus = 'Paid'" + ", " + 
                "orderStatus = '" + orderStatus + "'" + ", " +
                "end_datetime = " + "'" + end_datetime + "'" + ", " +
                "contacts = " + "'" + contactDetails + "'" +
                " WHERE " +
                "paymentStatus = 'Not Paid' AND tableNumber = " + table_Num + ";";
            MySqlCommand sql_cmd;   // MySQL Command Object Holder
            MySqlConnection conn = dbConn.GetConnection(); // MySQL Connnction Object

            // Execute SQL
            try
            {
                // Open/Start Database Connection
                conn.Open();

                // Set SQL Command
                sql_cmd = mySQL.cmd_set_connection(sql_stmt, conn);

                // Execute and return numbr of tables affected
                result = sql_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            // Return status
            return result;
        }
        public List<List<String>> get_Orders(int table_Num)
        {
            /*
             * Get current orders not paid
             */

            // Initialize Variables
            List<List<String>> order_data = new List<List<String>>(); // A Temporary List container to hold the rows returned from the search query
            DataSet ds = new DataSet(); // Container to store Data (generally from a Database)
            MySqlDataAdapter da; // MySQL Data Container used to hold data before connection usage
            MySqlConnection conn = dbConn.GetConnection();

            // Retrieve Order data
            string sql_stmt = "SELECT * FROM orders WHERE " +
                "paymentStatus = 'Not Paid' AND tableNumber = '" + table_Num + "';";

            // Execute Statement
            try
            {
                // Open Database Connection
                conn.Open();

                // Map SQL Statement to Database Connection and
                // Return the Data Adapter Object
                da = mySQL.adapter_set_query(sql_stmt, conn);

                // Populate Data Set with results in the data adapter
                da.Fill(ds);

            }
            catch(Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                // Close Database Connection after usage
                dbConn.CloseConnection(conn);
            }

            if(ds.Tables[0].Rows.Count > 0)
            {
                // If Results were found

                // Set result as order data and convert it to type List<String> for return
                order_data = ds.Tables[0].ToList<List<List<String>>>()[0]; 
            }

            // Output
            return order_data;
        }
        public int get_number_of_orders()
        {
            /*
             * Get total number of rows in table 'order'
             */

            // Variables
            int number_of_rows = -1; // Default to -1
            string sql_stmt = "SELECT COUNT(*) As Count FROM orders;"; // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            // Get number of rows
            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                number_of_rows = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return number_of_rows;
        }

        /*
         * MenuItems Functions
         */
        public List<List<String>> GetMenuItems()
        {
            /*
			Retrieve all menu items with their prices in a List<List<String>> object
			*/

            // Variables
            List<List<String>> res = new List<List<String>>(); // List Object of type List<List<String>> to store all returned menu items
            string sql_stmt = "SELECT * FROM products;"; // Get all values from 'orders'

            // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);
  
            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string productID = dr["productID"].ToString();
                    string productName = dr["productName"].ToString();
                    string price = dr["price"].ToString();
                    string categoryID = dr["categoryID"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(productID);
                    curr_row.Add(productName);
                    curr_row.Add(price);
                    curr_row.Add(categoryID);

                    // Append current row to all MenuItems List
                    res.Add(curr_row);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return res;
        }
        public List<String> GetMenuItemByProductID(string productID)
        {
            /*
             * Get Menu Item, filtered according to ProductID 
             */

            // Variables
            List<String> product_details = new List<String>(); // Initialize List<String> object
            string sql_stmt = "SELECT * FROM products WHERE productID = " + productID + ";"; // Get all values from 'orders'

            // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_ProdID = dr["productID"].ToString();
                    string tmp_ProdName = dr["productName"].ToString();
                    string tmp_Price = dr["price"].ToString();
                    string tmp_CatID = dr["categoryID"].ToString();

                    // Append values received from current row column values
                    product_details.Add(tmp_ProdID);
                    product_details.Add(tmp_ProdName);
                    product_details.Add(tmp_Price);
                    product_details.Add(tmp_CatID);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return product_details;
        }
        public List<String> GetMenuItemByName(string productName)
        {
            /*
             * Get Menu Item, filtered according to ProductID 
             */

            // Variables
            List<String> product_details = new List<String>(); // Initialize List<String> object
            string sql_stmt = "SELECT * FROM products WHERE productName = " + "'" + productName + "'" + ";"; // Get all values from 'orders'

            // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_ProdID = dr["productID"].ToString();
                    string tmp_ProdName = dr["productName"].ToString();
                    string tmp_Price = dr["price"].ToString();
                    string tmp_CatID = dr["categoryID"].ToString();

                    // Append values received from current row column values
                    product_details.Add(tmp_ProdID);
                    product_details.Add(tmp_ProdName);
                    product_details.Add(tmp_Price);
                    product_details.Add(tmp_CatID);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return product_details;
        }

        /*
         * Orders Related
         */
        public int check_table_is_free(int table_Num)
        {
            // Variables
            int is_free = -1; // Default to -1
            string sql_stmt = "SELECT COUNT(*) As Count FROM orders" +
                " WHERE " +
                "tableNumber = " + table_Num + " AND " + "paymentStatus = 'Not Paid';"; // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            // Get number of rows
            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                is_free = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return is_free;
        }

        public DataSet GetAllOrders()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT tableNumber, finalPrice, paymentStatus, orderStatus, start_datetime, end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE NOT paymentStatus=@paymentStatus");
            sql.AppendLine(" ");
            sql.AppendLine("GROUP BY tableNumber, CASE WHEN orderStatus=@orderStatus THEN end_datetime ELSE orderStatus END");
            sql.AppendLine(" ");
            sql.AppendLine("ORDER BY start_datetime DESC");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("paymentStatus", "Not Paid");
                da.SelectCommand.Parameters.AddWithValue("orderStatus", "Completed");
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

        public DataSet SearchOrders(int tableNo)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT tableNumber, finalPrice, paymentStatus, orderStatus, start_datetime, end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND NOT paymentStatus=@paymentStatus");
            sql.AppendLine(" ");
            sql.AppendLine("GROUP BY tableNumber, CASE");
            sql.AppendLine(" ");
            sql.AppendLine("WHEN orderStatus=@completedStatus THEN end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("WHEN orderStatus=@suspendStatus THEN end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("ELSE orderStatus END");
            sql.AppendLine(" ");
            sql.AppendLine("ORDER BY orderStatus ASC");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("tableNumber", tableNo);
                da.SelectCommand.Parameters.AddWithValue("paymentStatus", "Not Paid");
                da.SelectCommand.Parameters.AddWithValue("completedStatus", "Completed");
                da.SelectCommand.Parameters.AddWithValue("suspendStatus", "Suspended");
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

        public DataSet GetAllActiveOrders()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT tableNumber, finalPrice, paymentStatus, orderStatus");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE orderStatus=@currStatus");
            sql.AppendLine(" ");
            sql.AppendLine("GROUP BY tableNumber");
            sql.AppendLine(" ");
            sql.AppendLine("ORDER BY tableNumber");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("currStatus", "Active");
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

        public DataSet GetActiveOrderDetails(int tableNo)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND orderStatus=@currStatus");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("tableNumber", tableNo);
                da.SelectCommand.Parameters.AddWithValue("currStatus", "Active");
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

        public DataSet GetOrderDetails(int tableNo, DateTime end_datetime)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND end_datetime=@end_datetime");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("tableNumber", tableNo);
                da.SelectCommand.Parameters.AddWithValue("end_datetime", end_datetime);
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

        public DataSet GetTotalBill(int tableNo, string orderStatus)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT start_datetime, finalPrice");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND orderStatus=@orderStatus");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("tableNumber", tableNo);
                da.SelectCommand.Parameters.AddWithValue("orderStatus", orderStatus);
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

        public DataSet GetTotalBill(int tableNo, DateTime end_datetime)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT start_datetime, finalPrice");
            sql.AppendLine(" ");
            sql.AppendLine("FROM orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND end_datetime=@end_datetime");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("tableNumber", tableNo);
                da.SelectCommand.Parameters.AddWithValue("end_datetime", end_datetime);
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

        public int UpdateStatus(string currStatus)
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Orders");
            sql.AppendLine(" ");
            sql.AppendLine("SET paymentStatus=@paymentStatus, orderStatus=@orderStatus, end_datetime=@end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND orderStatus=@currStatus");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@tableNumber", this.TableNumber);
                sqlCmd.Parameters.AddWithValue("@currStatus", currStatus);
                sqlCmd.Parameters.AddWithValue("@paymentStatus", this.PaymentStatus);
                sqlCmd.Parameters.AddWithValue("@orderStatus", this.OrderStatus);
                sqlCmd.Parameters.AddWithValue("@end_datetime", this.End_datetime);
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

        public int CloseOrder()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Orders");
            sql.AppendLine(" ");
            sql.AppendLine("SET orderStatus=@orderStatus, end_datetime=@end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND orderStatus=@currStatus");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@tableNumber", this.TableNumber);
                sqlCmd.Parameters.AddWithValue("@end_datetime", this.End_datetime);
                sqlCmd.Parameters.AddWithValue("@currStatus", "Active");
                sqlCmd.Parameters.AddWithValue("@orderStatus", this.OrderStatus);
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

        /*
         * Payments
         */
        public string get_discount(string coupon_code)
        {
            /*
             * Verify coupon code exists and is valid by referencing table 'coupons' 
             * - Retrieve Discount Amount
             */


            /*
             * Get current orders
             */

            // Define SQL query statement
            string sql_stmt = "SELECT discount FROM coupons WHERE code = '" + coupon_code + "'" + ";";

            // Initialize Variables
            DataSet ds = new DataSet(); // Container to store Data (generally from a Database)
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);
            MySqlDataReader reader;
            string res = "";

            try
            {
                // Open Database connection
                conn.Open();

                // Initialize Dat Reader
                reader = cmd.ExecuteReader();

                // Fetch query result
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res = reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return res;
        }

        /*
         * Owner Report Functions
         */
        public int get_number_of_visits_by_Contacts(string contacts)
        {
            /*
             * Get number of visits made by 'contacts'
             */

            // Variables
            int number_of_rows = -1; // Default to -1
            string count = "0";
            string sql_stmt = "SELECT Count(DISTINCT start_datetime, end_datetime) As Count FROM orders" + " WHERE " + 
                "contacts = " + "'" + contacts + "'" + ";"; // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            // Get number of rows
            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    count = dr["Count"].ToString();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return int.Parse(count);
        }
        public List<String> get_all_contacts()
        {
            /*
             * Get all DISTINCT contacts
             */
            List<String> dataset = new List<string>();
            string sql_stmt = "SELECT DISTINCT contacts FROM orders;";

            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_Contacts = dr["contacts"].ToString();

                    // Append values received to dataset
                    dataset.Add(tmp_Contacts);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }

        public List<List<String>> get_all_visits_by_Contacts(string contacts)
        {
            /*
             * Retrieve all visits by a contact per visit and get
             * - Table Number
             * - Start Date Time
             * - End Date Time
             * - Final Price
             * - Number of items purchased
             * 
             * Grouped by all occurences of [start_datetime] and their table numbers being > 1
             */

            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT tableNumber, start_datetime, end_datetime, finalPrice, COUNT(contacts) AS Count FROM orders" +
                " WHERE " + "contacts = " + "'" + contacts + "'" + "GROUP BY start_datetime HAVING tableNumber > 1;";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_tableNumber = dr["tableNumber"].ToString();
                    string tmp_startDateTime = dr["start_datetime"].ToString();
                    string tmp_endDateTime = dr["end_datetime"].ToString();
                    string tmp_finalPrice = dr["finalPrice"].ToString();
                    string tmp_Count = dr["Count"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_tableNumber);
                    curr_row.Add(tmp_startDateTime);
                    curr_row.Add(tmp_endDateTime);
                    curr_row.Add(tmp_finalPrice);
                    curr_row.Add(tmp_Count);

                    // Append current row to result dataset
                    dataset.Add(curr_row);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<List<String>> get_daily_average_spending(string contacts)
        {
            /*
             * Get Daily Average Spending
             */
            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT contacts, DAY(start_datetime) as start_Day, DAY(end_datetime) as end_Day, AVG(finalPrice) as avg_Spending" + " " +
                "FROM orders" + " " + 
                "WHERE contacts = " + "'" + contacts + "'" + " " +
                "GROUP BY contacts, DAY(start_datetime);";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_contacts = dr["contacts"].ToString();
                    string tmp_start_day_Count = dr["start_Day"].ToString();
                    string tmp_end_day_Count = dr["end_Day"].ToString();
                    string tmp_avg_Spending = dr["avg_Spending"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_contacts);
                    curr_row.Add(tmp_start_day_Count);
                    curr_row.Add(tmp_end_day_Count);
                    curr_row.Add(tmp_avg_Spending);

                    // Append current row to result dataset
                    dataset.Add(curr_row);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<List<String>> get_weekly_average_spending(string contacts)
        {
            /*
             * Get Daily Average Spending
             */
            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT contacts, WEEK(start_datetime) as start_Week, WEEK(end_datetime) as end_Week, AVG(finalPrice) as avg_Spending" + " " +
                "FROM orders" + " " +
                "WHERE " + "contacts = " + "'" + contacts + "'" + " " +
                "GROUP BY" + " " +
                "contacts, WEEK(start_datetime);";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_contacts = dr["contacts"].ToString();
                    string tmp_start_week_Count = dr["start_Week"].ToString();
                    string tmp_end_week_Count = dr["end_Week"].ToString();
                    string tmp_avg_Spending = dr["avg_Spending"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_contacts);
                    curr_row.Add(tmp_start_week_Count);
                    curr_row.Add(tmp_end_week_Count);
                    curr_row.Add(tmp_avg_Spending);

                    // Append current row to result dataset
                    dataset.Add(curr_row);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<List<String>> get_monthly_average_spending(string contacts)
        {
            /*
             * Get Daily Average Spending
             */
            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT contacts, MONTH(start_datetime) as start_Month, MONTH(end_datetime) as end_Month, AVG(finalPrice) as avg_Spending" + " " + 
                "FROM orders" + " " + 
                "WHERE contacts = " + "'" + contacts + "'" + 
                "GROUP BY contacts, MONTH(start_datetime);";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_contacts = dr["contacts"].ToString();
                    string tmp_start_month_Count = dr["start_Month"].ToString();
                    string tmp_end_month_Count = dr["end_Month"].ToString();
                    string tmp_avg_Spending = dr["avg_Spending"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_contacts);
                    curr_row.Add(tmp_start_month_Count);
                    curr_row.Add(tmp_end_month_Count);
                    curr_row.Add(tmp_avg_Spending);

                    // Append current row to result dataset
                    dataset.Add(curr_row);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<List<String>> get_visit_datetime(string contacts)
        {
            /*
             * Get Contact's visit datetimes
             */

            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT contacts, tableNumber, start_datetime, end_datetime"  + " " + "FROM orders" + 
                " WHERE " + 
                "contacts = " + "'" + contacts + "'" + 
                " GROUP BY " + "start_datetime, end_datetime;";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_contacts = dr["contacts"].ToString();
                    string tmp_tableNumber = dr["tableNumber"].ToString();
                    string tmp_startDateTime = dr["start_datetime"].ToString();
                    string tmp_endDateTime = dr["end_datetime"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_contacts);
                    curr_row.Add(tmp_tableNumber);
                    curr_row.Add(tmp_startDateTime);
                    curr_row.Add(tmp_endDateTime);

                    // Append current row to result dataset
                    dataset.Add(curr_row);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<List<String>> get_weekly_visits(string contacts)
        {
            /*
             * Get Total visits per week
             */

            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT contacts, start_datetime, end_datetime, COUNT(DISTINCT start_datetime) As weekly_Count" + " " + 
                "FROM orders" + " " +
                "WHERE" + " " +
                "contacts = " + "'" + contacts + "'" + " " +
                "GROUP BY " + "WEEK(start_datetime);";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_contacts = dr["contacts"].ToString();
                    string tmp_startDateTime = dr["start_datetime"].ToString();
                    string tmp_endDateTime = dr["end_datetime"].ToString();
                    string tmp_Count = dr["weekly_Count"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_contacts);
                    curr_row.Add(tmp_startDateTime);
                    curr_row.Add(tmp_endDateTime);
                    curr_row.Add(tmp_Count);

                    // Append current row to result dataset
                    dataset.Add(curr_row);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<List<String>> get_monthly_visits(string contacts)
        {
            /*
             * Get Total visits per month
             */

            // Variables
            int number_of_rows = -1; // Default to -1
            List<List<String>> dataset = new List<List<String>>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT contacts, start_datetime, end_datetime, COUNT(DISTINCT start_datetime) As monthly_Count" + " " + 
                "FROM orders" + " " +
                "WHERE" + " " +
                "contacts = " + "'" + contacts + "'" + " " +
                "GROUP BY " + "MONTH(start_datetime);";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_contacts = dr["contacts"].ToString();
                    string tmp_startDateTime = dr["start_datetime"].ToString();
                    string tmp_endDateTime = dr["end_datetime"].ToString();
                    string tmp_Count = dr["monthly_Count"].ToString();

                    // Append values received from current row column values
                    curr_row.Add(tmp_contacts);
                    curr_row.Add(tmp_startDateTime);
                    curr_row.Add(tmp_endDateTime);
                    curr_row.Add(tmp_Count);

                    // Append current row to result dataset
                    dataset.Add(curr_row);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public List<String> get_last_Entry(string contact)
        {
            /*
             * Get Last Entry/Row by contact
             */
            // Variables
            int number_of_rows = -1; // Default to -1
            List<String> dataset = new List<String>(); // Container to store results of SQL Query

            // Define Connection String
            string sql_stmt = "SELECT tableNumber, start_datetime, end_datetime, finalPrice" + " " +
                "FROM orders WHERE contacts = " + "'" + contact + "'" + " " +
                "GROUP BY start_datetime, end_datetime" + " " + 
                "ORDER BY orderID DESC LIMIT 1;";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    string tmp_tableNumber = dr["tableNumber"].ToString();
                    string tmp_startDateTime = dr["start_datetime"].ToString();
                    string tmp_endDateTime = dr["end_datetime"].ToString();
                    string tmp_finalPrice = dr["finalPrice"].ToString();

                    // Append values to dataset
                    dataset.Add(tmp_tableNumber);
                    dataset.Add(tmp_startDateTime);
                    dataset.Add(tmp_endDateTime);
                    dataset.Add(tmp_finalPrice);

                    number_of_rows += 1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return dataset;
        }
        public int get_customer_favourite_category(string contacts)
        {
            /*
             * Get the MAX(categoryID) of the specified customer 
             * to get the customer's preference 
             */
            // Variables
            int number_of_rows = -1; // Default to -1
            int customer_Preference = 0;

            // Define Connection String
            // string sql_stmt = "SELECT MAX(categoryID) As favourite FROM orders WHERE contacts = " + "'" + contacts + "'";
            string sql_stmt = "SELECT categoryID, COUNT(categoryID) AS `favourite`" + 
                "FROM orders" + " " + 
                "WHERE contacts = " + "'" + contacts + "'" + " " +
                "GROUP BY categoryID" + " " + 
                "ORDER BY `favourite`" + " " + 
                "DESC LIMIT 1;";
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                cmd.ExecuteNonQuery();

                // Fetch query result
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    List<String> curr_row = new List<String>(); // List Object of type List<String> to store current row

                    // Read Data
                    // Get Values from column names
                    customer_Preference = int.Parse(dr["categoryID"].ToString());
                    // customer_Preference = int.Parse(dr["favourite"].ToString());
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return customer_Preference;
        }
    }
}