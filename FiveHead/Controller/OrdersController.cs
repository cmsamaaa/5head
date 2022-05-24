using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System;
using System.Collections.Generic;
using System.Data;

namespace FiveHead.Controller
{
    public class OrdersController
    {
		// Global Variables
		Order order = new Order();

		// Functions
		public int CreateOrder(Product product, int tableNumber, int productQty, double finalPrice, string couponCode, string contacts)
        {
			order = new Order(tableNumber, product.ProductID, product.CategoryID, product.ProductName, productQty, product.Price, DateTime.Now, DateTime.Now, finalPrice, couponCode, contacts);
			return order.CreateOrder();
        }

		public List<Order> GetPaymentBill(int tableNumber)
        {
			order = new Order();
			DataSet ds = order.GetPaymentBill(tableNumber, "Not Paid");

			if (ds.Tables[0].Rows.Count > 0)
				return ds.Tables[0].ToList<Order>();
			else
				return null;
		}

		public int UpdatePayment(int tableNo)
        {
			order = new Order(tableNo, "Paid");
			return order.UpdatePayment();
		}

		public int ClearPendingOrders(int tableNumber)
        {
			order = new Order(tableNumber, "Not Paid");
			return order.DeleteOrders();
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

			// Get all visits
			List<List<String>> visits = order.get_weekly_visits(contacts);

			// Calculate Weekly
			for (int i = 0; i < visits.Count; i++)
			{
				List<String> curr_row_visit = visits[i]; // Current Visit details by Contact

				// Count
				int count = int.Parse(curr_row_visit[3]);

				weekly_visits += count;
			}

			return weekly_visits;
		}
		public int CalculateMonthlyVisits(string contacts)
        {
			/*
			 * Calculate monthly visits
			 */
			int monthly_visits = 0;

			// Get all visits
			List<List<String>> visits = order.get_monthly_visits(contacts);

			// Calculate Monthly
			for (int i = 0; i < visits.Count; i++)
			{
				List<String> curr_row_visit = visits[i]; // Current Visit details by Contact

				// Count
				int count = int.Parse(curr_row_visit[3]);

				monthly_visits += count;
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