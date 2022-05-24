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

        public Order(int tableNumber, string paymentStatus)
        {
            this.TableNumber = tableNumber;
            this.PaymentStatus = paymentStatus;
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

        public Order(int orderID, int tableNumber, int productID, int categoryID, string productName, int productQty, double price, DateTime start_datetime, DateTime end_datetime, string paymentStatus, string orderStatus, double finalPrice, string couponCode, string contacts) 
            : this(tableNumber, productID, categoryID, productName, productQty, price, start_datetime, end_datetime, finalPrice, couponCode, contacts)
        {
            this.OrderID = orderID;
            this.PaymentStatus = paymentStatus;
            this.OrderStatus =  orderStatus;
        }

        public Order(Order order) 
            : this(order.orderID, order.tableNumber, order.productID, order.categoryID, order.productName, order.productQty, order.price, order.start_datetime, order.end_datetime, order.paymentStatus, order.orderStatus, order.finalPrice, order.couponCode, order.contacts)
        {
            if(order == null)
                throw new ArgumentNullException();
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

        public DataSet GetPaymentBill(int tableNumber, string paymentStatus)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND paymentStatus=@paymentStatus");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("tableNumber", tableNumber);
                da.SelectCommand.Parameters.AddWithValue("paymentStatus", paymentStatus);
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

        public int UpdatePayment()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Orders");
            sql.AppendLine(" ");
            sql.AppendLine("SET paymentStatus=@newPaymentStatus, orderStatus=@orderStatus, end_datetime=@end_datetime");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND paymentStatus=@paymentStatus");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@tableNumber", this.TableNumber);
                sqlCmd.Parameters.AddWithValue("@end_datetime", DateTime.Now);
                sqlCmd.Parameters.AddWithValue("@paymentStatus", "Not Paid");
                sqlCmd.Parameters.AddWithValue("@newPaymentStatus", this.PaymentStatus);
                sqlCmd.Parameters.AddWithValue("@orderStatus", "Active");
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
        public int DeleteOrders()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("DELETE");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Orders");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE tableNumber=@tableNumber AND paymentStatus=@paymentStatus");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@tableNumber", this.TableNumber);
                sqlCmd.Parameters.AddWithValue("@paymentStatus", this.PaymentStatus);
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
         * Owner Report Functions
         */
        public int get_number_of_visits_by_Contacts(string contacts)
        {
            /*
             * Get number of visits made by 'contacts'
             */

            // Variables
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