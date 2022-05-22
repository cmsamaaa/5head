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

		public DataSet GetAllOrders()
        {
			return order.GetAllOrders();
        }

		public DataSet SearchOrders(int tableNo)
        {
			return order.SearchOrders(tableNo);
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
			order = new Order(tableNumber, "Refunded", "Suspended", DateTime.Now);
			return order.UpdateStatus("Active");
		}

		/*
		 * Payment-related
		 */
		public string GetDiscount(string discount_code)
        {
			return order.get_discount(discount_code);
        }

		/*
		 * Report-related
		 */
		public List<List<String>> GetVisitsByContact(string contacts)
        {
			return order.get_all_visits_by_Contacts(contacts);
        }
		public int GetNumberOfVisitsByContacts(string contacts)
        {
			return order.get_number_of_visits_by_Contacts(contacts);
        }
		public List<String> GetAllContacts()
        {
			/*
			 * Get all distinct contacts
			 */
			return order.get_all_contacts();
        }
		public double CalculateTotalSpend(string contacts)
        {
			/*
			 * Calculate total spending
			 */
			List<List<String>> all_visits = GetVisitsByContact(contacts);
			double totalSpend = 0.0;

			/*
			 * Calculate Total Spending
			 */
			for (int i = 0; i < all_visits.Count; i++)
			{
				/*
				 * Current Row
				 */
				List<String> curr_row = all_visits[i];
				string tmp_finalPrice = curr_row[3];
				totalSpend += double.Parse(tmp_finalPrice); // Calculate Total Spending
			}

			// Return
			return totalSpend;
		}
		public double CalculateAverageSpend(string contacts)
        {
			/*
			 * Calculate and retrieve average spend per visit
			 * Formula:
			 *	average Spending : [total_spending / number of visits]
			 */
			List<List<String>> all_visits = GetVisitsByContact(contacts);
			int total_visits = GetNumberOfVisitsByContacts(contacts);
			double totalSpend = 0.0;
			double avgSpend = 0.0;

			/*
			 * Calculate Total Spending
			 */
			for(int i=0; i < all_visits.Count; i++)
            {
				/*
				 * Current Row
				 */
				List<String> curr_row = all_visits[i];
				string tmp_finalPrice = curr_row[3];
				totalSpend += double.Parse(tmp_finalPrice); // Calculate Total Spending
            }

			/*
			 * Calculate Average Spending
			 */
			avgSpend = totalSpend / total_visits;

			// Return
			return avgSpend;
        }
		public List<List<String>> GetDailyAverageSpend(string contacts)
		{
			/*
			 * Calculate and retrieve average spending per day
			 */
			return order.get_daily_average_spending(contacts);
		}
		public List<List<String>> GetWeeklyAverageSpend(string contacts)
		{
			/*
			 * Calculate and retrieve average spending per week
			 */
			return order.get_weekly_average_spending(contacts);
		}
		public List<List<String>> GetMonthlyAverageSpend(string contacts)
		{
			/*
			 * Calculate and retrieve average spending per month
			 */
			return order.get_monthly_average_spending(contacts);
		}
		public int CalculateDaysSinceLastVisit(string contacts)
        {
			/*
			 * Calculate number of days since last visit
			 */

			// Get Last Entry (last visit)
			List<String> last_Entry = order.get_last_Entry(contacts);

			string last_entry_startDateTime = last_Entry[1];
			string last_entry_endDateTime = last_Entry[2];

			DateTime dt_to_Check = Convert.ToDateTime(last_entry_startDateTime);
			DateTime now = DateTime.Now;

			int days_since_last_Visit = (now - dt_to_Check).Days;
			return days_since_last_Visit;
		}

		public int CalculateDailyVisits(string contacts)
        {
			/*
			 * Calculate daily visits
			 */
			int daily_visits = 0;
			DateTime prev_start_dt;
			DateTime prev_end_dt;

			// Get all visits
			List<List<String>> visits = order.get_visit_datetime(contacts);

			// Calculate Daily
			for(int i=0; i < visits.Count; i++)
            {
				List<String> curr_row_visit = visits[i]; // Current Visit details by Contact

				/* Split Start date-time string into array
				 * 0 : Date
				 * 1 : Time
				 */
				string start_dt = curr_row_visit[2].ToString(); // Start DateTime
				string[] start_dt_arr = start_dt.Split(' ');    // Start DateTime array

				// Split Date into individual components (Year, Month, Date)
				string[] start_date_arr = start_dt_arr[0].Split('/');
				int start_Date = int.Parse(start_date_arr[0]);
				int start_Month = int.Parse(start_date_arr[1]);
				int start_Year = int.Parse(start_date_arr[2]);

				/* Split End date-time string into array
				 * 0 : Date
				 * 1 : Time
				 */
				string end_dt = curr_row_visit[3].ToString();   // End DateTime
				string[] end_dt_arr = end_dt.Split(' ');

				// Split Date into individual components (Year, Month, Date)
				string[] end_date_arr = end_dt_arr[0].Split('/');
				int end_Date = int.Parse(end_date_arr[0]);
				int end_Month = int.Parse(end_date_arr[1]);
				int end_Year = int.Parse(end_date_arr[2]);

				// Compare DateTime
				// If 0 : Same Date
				// If < 0 : Date Start is earlier than Date End
				// If > 0 : Date Start is later than Date End
				DateTime dStart = new DateTime(start_Year, start_Month, start_Date);
				prev_start_dt = dStart;
				DateTime dEnd = new DateTime(end_Year, end_Month, end_Date);
				prev_end_dt = dEnd;

				if(prev_start_dt == dStart.AddDays(1))
                {
					/* Same Date */
					daily_visits++;
				}
            }

			return daily_visits;
		}
		public int CalculateWeeklyVisits(string contacts)
        {
			/*
			 * Calculate weekly visits
			 */
			int weekly_visits = 0;
			DateTime prev_start_dt;
			DateTime prev_end_dt;

			// Get all visits
			List<List<String>> visits = order.get_visit_datetime(contacts);

			// Calculate Weekly
			for (int i = 0; i < visits.Count; i++)
			{
				List<String> curr_row_visit = visits[i]; // Current Visit details by Contact

				/* Split Start date-time string into array
				 * 0 : Date
				 * 1 : Time
				 */
				string start_dt = curr_row_visit[2].ToString(); // Start DateTime
				string[] start_dt_arr = start_dt.Split(' ');    // Start DateTime array

				// Split Date into individual components (Year, Month, Date)
				string[] start_date_arr = start_dt_arr[0].Split('/');
				int start_Date = int.Parse(start_date_arr[0]);
				int start_Month = int.Parse(start_date_arr[1]);
				int start_Year = int.Parse(start_date_arr[2]);

				/* Split End date-time string into array
				 * 0 : Date
				 * 1 : Time
				 */
				string end_dt = curr_row_visit[3].ToString();   // End DateTime
				string[] end_dt_arr = end_dt.Split(' ');

				// Split Date into individual components (Year, Month, Date)
				string[] end_date_arr = end_dt_arr[0].Split('/');
				int end_Date = int.Parse(end_date_arr[0]);
				int end_Month = int.Parse(end_date_arr[1]);
				int end_Year = int.Parse(end_date_arr[2]);

				// Compare DateTime
				// If 0 : Same Date
				// If < 0 : Date Start is earlier than Date End
				// If > 0 : Date Start is later than Date End
				DateTime dStart = new DateTime(start_Year, start_Month, start_Date);
				prev_start_dt = dStart;
				DateTime dEnd = new DateTime(end_Year, end_Month, end_Date);
				prev_end_dt = dEnd;

				if (prev_start_dt == dStart.AddDays(7))
				{
					/* Same Date */
					weekly_visits++;
				}
			}

			return weekly_visits;
		}
		public int CalculateMonthlyVisits(string contacts)
        {
			/*
			 * Calculate monthly visits
			 */
			int monthly_visits = 0;
			DateTime prev_start_dt;
			DateTime prev_end_dt;

			// Get all visits
			List<List<String>> visits = order.get_visit_datetime(contacts);

			// Calculate Monthly
			for (int i = 0; i < visits.Count; i++)
			{
				List<String> curr_row_visit = visits[i]; // Current Visit details by Contact

				/* Split Start date-time string into array
				 * 0 : Date
				 * 1 : Time
				 */
				string start_dt = curr_row_visit[2].ToString(); // Start DateTime
				string[] start_dt_arr = start_dt.Split(' ');    // Start DateTime array

				// Split Date into individual components (Year, Month, Date)
				string[] start_date_arr = start_dt_arr[0].Split('/');
				int start_Date = int.Parse(start_date_arr[0]);
				int start_Month = int.Parse(start_date_arr[1]);
				int start_Year = int.Parse(start_date_arr[2]);

				/* Split End date-time string into array
				 * 0 : Date
				 * 1 : Time
				 */
				string end_dt = curr_row_visit[3].ToString();   // End DateTime
				string[] end_dt_arr = end_dt.Split(' ');

				// Split Date into individual components (Year, Month, Date)
				string[] end_date_arr = end_dt_arr[0].Split('/');
				int end_Date = int.Parse(end_date_arr[0]);
				int end_Month = int.Parse(end_date_arr[1]);
				int end_Year = int.Parse(end_date_arr[2]);

				// Compare DateTime
				// If 0 : Same Date
				// If < 0 : Date Start is earlier than Date End
				// If > 0 : Date Start is later than Date End
				DateTime dStart = new DateTime(start_Year, start_Month, start_Date);
				prev_start_dt = dStart;
				DateTime dEnd = new DateTime(end_Year, end_Month, end_Date);
				prev_end_dt = dEnd;

				if (prev_start_dt == dStart.AddMonths(1))
				{
					/* Same Date */
					monthly_visits++;
				}
			}

			return monthly_visits;
        }
		public List<String> CalculatePreferences(string contacts)
        {
			/*
			 * Calculate contact/customer's preferences
			 * - Get categoryID of the MAX()  by the customer
			 * - Get Category name from table 'categories'
			 */
			List<String> ret = new List<String>();

			// Get Customer's Preference/Favourite Item Category
			int catID = order.get_customer_favourite_category(contacts);

			// Get Category Name
			CategoriesController cat_Controller = new CategoriesController();
			string cat_Name = cat_Controller.GetCategoryNameByID(catID).ToString();

			// Package result
			ret.Add(catID.ToString());
			ret.Add(cat_Name);

			// Return result
			return ret;
        }
	}
}