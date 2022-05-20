using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace FiveHead.Controller
{
    public class OrdersController
    {
		// Global Variables
		Order order = new Order();        
		//float final_Price = 0.0f; // Temporary placeholder for calculating Final Price

		// Constructor
		public OrdersController()
        {
			// Initialize
        }

		// Functions
		public int CheckoutCart(string coupon_Code, List<List<String>> cart)
		{
			/*
			 * Take all values in 'cart' and Insert into table 
			 */
			int ret_Code = 0; // Return '1' => Success | '0' => Error
			int cart_size = cart.Count;
			string errMsg = "";

			if (cart_size > 0)
			{
				try
				{
					// If at least 1 item is found
					for (int i = 0; i < cart_size; i++)
					{
						List<String> curr_order = cart[i];

						int orderID = int.Parse(curr_order[0]);
						int table_Num = int.Parse(curr_order[1]);
						int productID = int.Parse(curr_order[2]);   // Convert String to Integer
						int categoryID = int.Parse(curr_order[3]);  // Convert String to Integer
						string productName = curr_order[4];
						int productQty = int.Parse(curr_order[5]);
						double price = double.Parse(curr_order[6]);  // Convert String to Float
						string start_datetime = curr_order[7];
						string end_datetime = curr_order[8];
						string status = curr_order[9];
						string orderStatus = curr_order[10];
						double finalPrice = double.Parse(curr_order[11]);
						string contacts = curr_order[12];

						/* #DEBUG : Test out input
						for(int j=0; j < curr_order.Count; j++)
						{
							System.Diagnostics.Debug.WriteLine("[DEBUG] Item " + "[" + j.ToString() + "] | " + curr_order[j]);
						}
						*/
						ret_Code = order.insert_Orders(
							orderID, table_Num, productID, categoryID,
							productName, productQty, price, start_datetime,
							end_datetime, status, orderStatus, finalPrice, contacts
						);

						// System.Diagnostics.Debug.WriteLine(res);
					}

					ret_Code = 1;
				}
				catch (Exception ex)
                {
					errMsg = ex.Message.ToString();
                }
			}
			return ret_Code;
		}
		public float CalculateFinalPrice(List<List<String>> cart)
        {
			/*
			 * Get all cart items and calculate all price
			 */
			float calc_Price = 0.0f;
			float final_Price = 0.0f;

			int cart_size = 0;
			if(cart != null)
            {
				cart_size = cart.Count;
			}

			if(cart_size > 0)
            {
				// More than 1 items

				for(int i=0; i < cart_size; i++)
                {
					// Get Current Element
					List<String> curr_item = cart[i];

					// Get Price of current item
					float curr_item_Price = float.Parse(curr_item[3]);

					// Accumulate to final price
					calc_Price += curr_item_Price;
                }
            }

			// Set final price
			final_Price = calc_Price;

			return final_Price;
        }

		/*
		 * Payment-related functions
		 * - i.e. Coupon validation
		 */
		public int ProcessPayment(int table_num, string orderStatus, string contactDetails, string end_datetime)
		{
			/*
			 * Make Payment
			 * 
			 * 1. Get all rows by customer
			 *		- Get all prices
			 * 2. Calculate Final Price
			 * 3. Request user to pay [final_price]
			 * 4. Confirm Payment is Made
			 * 5. Update status of all rows belonging to customer's [orderID] to 'Paid'
			 */
			int ret_Code = 0;

			// Update 'orders' table status with 'Paid'
			ret_Code = order.update_Orders(table_num, orderStatus, contactDetails, end_datetime);

			return ret_Code;
		}
		public int CheckTableIsFree(int tableNum)
        {
			return order.check_table_is_free(tableNum);
		}

		public List<List<String>> GetMenuItems()
        {
			return order.GetMenuItems();
        }
		public List<String> GetMenuItemByProductID(string productID)
        {
			return order.GetMenuItemByProductID(productID);
        }
		public List<String> GetMenuItemByName(string filter)
        {
			return order.GetMenuItemByName(filter);
        }
		public List<List<String>> GetOrders(int table_num)
        {
			return order.get_Orders(table_num);
        }
		public int GetNumberOfOrders()
        {
			return order.get_number_of_orders();
        }

		public DataSet SearchOrders(int tableNum)
        {
			return order.SearchOrders(tableNum);
        }

		public DataSet GetAllActiveOrders()
        {
			return order.GetAllActiveOrders();
        }

		public DataSet GetOrderDetails(int tableNo, DateTime end_datetime)
		{
			return order.GetOrderDetails(tableNo, end_datetime);
		}

		public DataSet GetActiveOrderDetails(int tableNo)
		{
			return order.GetActiveOrderDetails(tableNo);
		}

		public double GetTotalBill(int tableNo, string orderStatus)
        {
			DataSet ds = order.GetTotalBill(tableNo, orderStatus);
			DataTable dt = ds.Tables[0];

			double totalBill = 0;
			foreach (DataRow dr in dt.Rows)
				totalBill = totalBill + Convert.ToDouble(dr["finalPrice"].ToString());

			return totalBill;
		}

		public double GetTotalBill(int tableNo, DateTime end_datetime)
		{
			DataSet ds = order.GetTotalBill(tableNo, end_datetime);
			DataTable dt = ds.Tables[0];

			double totalBill = 0;
			foreach (DataRow dr in dt.Rows)
				totalBill = totalBill + Convert.ToDouble(dr["finalPrice"].ToString());

			return totalBill;
		}

		public int CompleteOrder(int tableNumber)
		{
			order = new Order(tableNumber, "Completed", DateTime.Now);
			return order.CloseOrder();
		}

		public int SuspendOrder(int tableNumber)
		{
			order = new Order(tableNumber, "Suspended");
			return order.UpdateStatus("Active");
		}

		/*
		 * Payment-related
		 */
		public string GetDiscount(string discount_code)
        {
			return order.get_discount(discount_code);
        }
	}
}