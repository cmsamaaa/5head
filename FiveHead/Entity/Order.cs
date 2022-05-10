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
        /*
         * Entity Objects
         */
        private int orderID;
        public Order()
        {
            /* Constructor */
        }

        public Order(int orderID)
        {
            this.OrderID = orderID;
        }

        public Order(Order order) : this(order.OrderID)
        {
            if(order == null)
            {
                throw new ArgumentNullException();
            }
        }

        public int OrderID
        {
            /*
             * Getter/Setter
             */
            get
            {
                return orderID;
            }
            set
            {
                orderID = value;
            }
        }

        /*
         * Class Variables
         */
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        /*
         * Function Definitions
         */
        public int insert_Orders(int orderID, int staffID, int productID, string productName, int categoryID, float price)
        {
            /*
             * Insert Orders into Table
             */

            // Variables
            string sql_stmt = "INSERT INTO orders VALUES (" +
                orderID + "," +
                staffID + "," +
                productID + "," +
                "'" + productName + "'" + "," + 
                categoryID + "," +
                price +
            ");";
            int result = 0;
            MySqlCommand sql_cmd;
            MySqlConnection conn = dbConn.GetConnection();

            // Execute SQL
            try
            {
                sql_cmd = mySQL.cmd_set_connection(sql_stmt, conn);
                conn.Open();
                result = sql_cmd.ExecuteNonQuery(); // Returns number of rows affected
            }
            catch(Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public int update_Orders(int orderID, int staffID)
        {
            /*
             * Update Order Status after payment is made		
             */

            // Variables
            int result = 0;
            string sql_stmt = "UPDATE orders SET status = 'Paid'" + " WHERE " +
                "orderID = '" + orderID + "'" + " AND " +
                "staffID = '" + staffID + "';";
            MySqlCommand sql_cmd;   // MySQL Command Object Holder
            MySqlConnection conn = dbConn.GetConnection(); // MySQL Connnction Object

            // Execute SQL
            try
            {
                sql_cmd = mySQL.cmd_set_connection(sql_stmt, conn);

                // Open/Start Database Connection
                conn.Open();

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
        List<String> get_Orders(int orderID, int staffID)
        {
            /*
             * Get current orders
             */

            // Initialize Variables
            List<String> order_data = new List<String>(); // A Temporary List container to hold the rows returned from the search query
            DataSet ds = new DataSet(); // Container to store Data (generally from a Database)
            MySqlDataAdapter da; // MySQL Data Container used to hold data before connection usage
            MySqlConnection conn = dbConn.GetConnection();

            // Retrieve Order data
            string sql_stmt = "SELECT * FROM orders WHERE status = 'Not Paid'" + " AND " +
                "orderID = '" + orderID + "'" + " AND " +
                "staffID = '" + staffID + "';";

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
                order_data = ds.Tables[0].ToList<List<String>>()[0]; 
            }

            // Output
            return order_data;
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
            List<String> curr_row = new List<String>(); // List Object to store current row returns
            string sql_stmt = "SELECT * FROM products;"; // Get all values from 'orders'

            // Define Connection String
            MySqlConnection conn = dbConn.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql_stmt, conn);

            try
            {
                // Open Database connection
                conn.Open();

                // Execute Query
                // Fetch query result
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Read Data

                    // Get Values from column names
                    curr_row.Add(reader["productID"].ToString());
                    curr_row.Add(reader["productName"].ToString());
                    curr_row.Add(reader["price"].ToString());
                    curr_row.Add(reader["categoryID"].ToString());

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
    }
}