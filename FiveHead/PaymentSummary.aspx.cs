using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FiveHead.Controller;
using System.Text;

namespace FiveHead
{
    public partial class PaymentSummary : System.Web.UI.Page
    {
        // Global Variables
        OrdersController ordersController = new OrdersController();         // For handling Orders (Customer)
        ProfilesController profilesController = new ProfilesController();   // For handling Profiles

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Check if page is first time loading
                // Will only execute the first time when the page loads

            }
			ViewAllOrders();
        }

        /* Event Handlers */
        protected void btn_make_Payment(object sender, EventArgs e)
        {
			/*
			 * When Button "btn_make_Payment" is clicked
			 */
			// Get Contact Details
			string contactDetails = tb_uInput_ContactDetails.Value;

            int ret_Code = MakePayment(contactDetails);

			if(ret_Code > 0)
            {
				// Clear Sessions
				Session.Clear();

				// Redirect back to Menu
				Response.Redirect("~/Menu.aspx");
			}
            else
            {
				lbl_Info.InnerText = "Error making payment.";
            }
        }
        protected void btn_go_to_Cart(object sender, EventArgs e)
        {
            /*
			 * Go To Menu
			 */
            Response.Redirect("~/Cart.aspx");
        }

        /* Helper Functions */
        public int MakePayment(string contactDetails)
        {
            /*
			 * User to Make Payment
			 */

			// Get Table Number
            int tableNum = int.Parse(Session["tableNum"].ToString());

			// Get Current DateTime for End
			DateTime now = DateTime.Now;
			string end_datetime = now.ToString("yyyy-MM-dd H:mm:ss");
			this.Session["end_datetime"] = end_datetime;

			int ret_Code = ordersController.ProcessPayment(tableNum, contactDetails, end_datetime);

            return ret_Code;
        }
		public void ViewAllOrders()
		{
			/*
			 * Display all items added to cart
			 */

			// Get Table Numbers
			string tableNum = Session["tableNum"].ToString();

			// Set Table Number
			lbl_tableNumber_Value.InnerText = tableNum;

			// List<List<String>> all_orders = (List<List<String>>)Session["cart"];
			// List<String> all_orders = ordersController.GetOrders(int.Parse(tableNum)); // Temporarily commented out to test out Proof-of-Concept with SESSION["checked_out_cart"]
			List<List<String>> all_orders = (List<List<String>>)Session["checked_out_cart"];
			List<List<String>> dataset = new List<List<String>>(); // Create master dataset for getting all rows to populate datagridview table
			int cart_size = 0;

			// Reset Table
			table_payment_Summary.Rows.Clear();

			// Null Check
			if (all_orders == null)
			{
				all_orders = new List<List<String>>();
			}
			else
			{
				cart_size = all_orders.Count;
			}
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			/*
			 * Gather dataset
			 */
			if (cart_size > 0)
			{
				for (int i = 0; i < cart_size; i++)
				{
					// Initialize Variables		
					List<String> curr_order = all_orders[i];	
					List<String> tmp = new List<String>(); // Create a new List to store data from the current row

					// Get Variables
					string curr_orderID = curr_order[0];
					string curr_table_num = curr_order[1];
					string curr_categoryID = curr_order[2];
					string productID = curr_order[3];
					string productName = curr_order[4];
					string productQty = curr_order[5];
					string price = curr_order[6];
					string status = curr_order[9];
					string finalPrice = curr_order[10];
					string contacts = curr_order[11];

					/*
					 * Data for table_payment_Summary
					 */
					// Add every index as a new line in the StringBuilder / table entry
					tmp.Add((i + 1).ToString());
					tmp.Add(productID);
					tmp.Add(curr_categoryID);
					tmp.Add(productName);
					tmp.Add(productQty);
					tmp.Add(price);
					tmp.Add(status);

					// Add temporary dataset to list of rows
					dataset.Add(tmp);
				}

				// Fill up user info
				double total_Price = double.Parse(all_orders[0][10]);
				double discounts = double.Parse(Session["discount"].ToString());
				double final_Price = total_Price - discounts;
				lbl_total_Price_Value.InnerText = "$" + all_orders[0][10].ToString();
				lbl_discount_Value.InnerText = "$" + discounts.ToString();
				lbl_final_Price_Value.InnerText = "$" + final_Price.ToString();

				/*
				 * Headers 
				 */
				// Create Table Header
				List<String> HeaderText = new List<String>() { "s/n", "Product ID", "Category ID", "Product Name", "Quantity", "Price", "Status" };

				// Create new Table Header Row
				TableHeaderRow header = new TableHeaderRow();
				for (int i = 0; i < HeaderText.Count; i++)
				{
					// Create new cell
					TableCell curr_header = new TableCell();

					// Populate Cell
					curr_header.Text = HeaderText[i];

					// Add Cell to Header Row
					header.Cells.Add(curr_header);
				}

				// Add Header Row to Table
				table_payment_Summary.Rows.Add(header);

				/*
				 * Rows 
				 */
				// Create Table Rows
				for (int i = 0; i < dataset.Count; i++)
				{
					/* 
					 * Get Current Row in Table Dataset
					 */
					List<String> curr_row = dataset[i];
					TableRow new_tablerow = new TableRow();
					for (int j = 0; j < curr_row.Count; j++)
					{
						/*
						 * Get Cells in current row
						 */
						TableCell new_cell = new TableCell();

						//table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
						new_cell.Text = curr_row[j];

						// Add Cell to row
						new_tablerow.Cells.Add(new_cell);
					}

					// Append Rows to table
					table_payment_Summary.Rows.Add(new_tablerow);
				}
			}
			else
			{
				// No Items Found
				TableRow curr_row = new TableRow();
				TableCell new_cell = new TableCell();
				new_cell.Text = "No Items in Cart. <br/>";
				curr_row.Cells.Add(new_cell);
				table_payment_Summary.Rows.Add(curr_row);
			}
		}
	}
}