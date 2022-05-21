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
    public partial class cart : System.Web.UI.Page
    {
		// Global Variables
		OrdersController ordersController = new OrdersController();			// For handling Orders (Customer)
		ProfilesController profilesController = new ProfilesController();	// For handling Profiles

        protected void Page_Load(object sender, EventArgs e)
        {
			ViewAllInCart();
        }

		// Button Event Functions
		protected void btn_checkout_Cart(object sender, EventArgs e)
		{
			/*
			 * When Button "btn_checkout_Cart" is clicked
			 * 
			 * 1. Take all orders (found in 'cart' List<List<String>> variable)
			 * 2. Insert into Table - row by row
			 */

			// Clear Label
			lbl_Debug_Output.Text = "";
			lbl_Info.InnerText = "";

			/* 
			 * Get Coupon Code
			 * - Cross-Reference with 'coupons' table to verify coupon code's validity
			 * - if valid : Give discount according to the 'discount value' column
			 */
			string coupon_Code = tb_coupon_Code.Value;

			int orderID = (ordersController.GetNumberOfOrders() + 1);
			List<List<String>> cart = (List<List<String>>)Session["cart"];

			/*
			 * Filter and get all cart details
			 */
			List<String> required_columns = new List<string>() { 
				"orderID", "tableNumber", "productID", "categoryID", 
				"productName", "productQty", "price", "start_datetime", 
				"end_datetime", "paymentStatus",  "orderStatus", "finalPrice", "contacts" 
			};
			List<List<String>> conf_cart = new List<List<String>>(); // To store the confirmed cart values

			// Get Table Number
			string tableNum = "";
			if (Session["tableNum"] == null)
            {
				tableNum = tb_table_Number.Value;
			}
			else
			{
				// Table is already assigned
				tableNum = Session["tableNum"].ToString();

				// Set table objects to invisible
				// - so that user cannot change table manually after confirmation
				lb_table_Number.Visible = false;
				table_Shopping_Cart.Visible = false;
			}

			// Get Total Price
			float final_Price = ordersController.CalculateFinalPrice(cart);
			StringBuilder table_Text = new StringBuilder();
			table_Text.AppendLine("Final Price: " + final_Price.ToString());

			// Get Start DateTime
			string start_Datetime = Session["start_datetime"].ToString();
			string end_Datetime = start_Datetime;

			// Data Verification
			if (orderID != -1)
			{
				// No Orders found, default to 1st row
				orderID = 0;
			}

			if(tableNum == "")
            {
				// Default
				Random r = new Random();
				tableNum = r.Next(0, 100).ToString();
            }

			/*
			 * Check if table number exists AND status is 'Not Paid'
			 * - This means the table is occupied, get another table number
			 */
			int table_number_exists = -1;
			while (table_number_exists > 0)
			{
				table_number_exists = ordersController.CheckTableIsFree(int.Parse(tableNum)); // Check if table number is empty
				if (table_number_exists > 0)
				{
					// Table is occupied, get another table
					Random r = new Random();
					tableNum = r.Next(0, 100).ToString();
				}
			}

			// Get Discount and write to session
			string discount = "0";
			if (coupon_Code != "")
			{
				discount = ordersController.GetDiscount(coupon_Code);
			}

			// Validate Discount
			int discount_int = 0;
			double disc_Price = 0.0;
			if(int.TryParse(discount, out discount_int))
            {
				// Calculate Discounted Price
				disc_Price = final_Price - discount_int;
			}
			double disc_price_Rounded = Math.Round((Double)disc_Price, 2); // Round price to 2 decimal places
			if (disc_price_Rounded < 0) // Data Validation : No negative values
			{
				disc_price_Rounded = 0.0;
			}

			/*
                * Get other details and
                * populate final cart
                */
			if (cart != null)
			{
				/*
				 * Populate Final Cart
				 */
				for (int i = 0; i < cart.Count; i++)
				{
					List<String> curr_row = cart[i];
					List<String> tmp = new List<String>();
					int curr_prod_total_Quantity = 0; // Total quantity of a certain item

					// Get details from cart
					string curr_prodID = curr_row[0];
					string curr_catID = curr_row[1];
					string curr_prodName = curr_row[2];
					string curr_prodPrice = curr_row[3];
					string curr_prodStatus = curr_row[4];

					// Calculate Product Quantity
					for (int j = 0; j < cart.Count; j++)
					{
						// Loop through cart to count occurence of current item
						List<String> row_to_check = cart[j];
						string curr_row_prodID = row_to_check[0];
						// If found, add 1
						if (curr_prodID == curr_row_prodID)
						{
							curr_prod_total_Quantity += 1;
						}
					}

					// Check if product exists in final cart
					int final_cart_curr_size = conf_cart.Count;
					bool element_exists = false;

					if (final_cart_curr_size > 0)
					{
						/*
						 * Verify that product doesnt exist inside the final cart yet
						 * - if doesnt exist : Add
						 */
						for (int j = 0; j < final_cart_curr_size; j++)
						{
							List<String> row_to_check = conf_cart[j];
							string curr_row_prodID = row_to_check[2];

							// Check if Product ID exists already in the final cart
							// Filter to have only one of each item in the conf_cart
							if (row_to_check.Contains(curr_prodName))
							{
								element_exists = true;
							}
						}
					}

					if (!element_exists)
					{
						/*
						 * Does not exist
						 */
						// If doesnt exist
						// Populate final cart
						tmp.Add(orderID.ToString());
						tmp.Add(tableNum.ToString());
						tmp.Add(curr_prodID);
						tmp.Add(curr_catID);
						tmp.Add(curr_prodName);
						tmp.Add(curr_prod_total_Quantity.ToString());
						tmp.Add(curr_prodPrice);
						tmp.Add(start_Datetime);
						tmp.Add(end_Datetime); // Default to '[START_DATETIME]', to be updated when payment is made, alongside Status
						tmp.Add(curr_prodStatus);
						tmp.Add("Not Active");
						tmp.Add(disc_price_Rounded.ToString());
						tmp.Add("Not Provided"); // Default to 'Not Provided', to be updated when payment is made, alongside Status

						// Write to final cart
						conf_cart.Add(tmp);

						lbl_Debug_Output.Text += curr_prodName + " Has been added" + "<br/>";
					}
					else
                    {
						lbl_Debug_Output.Text += curr_prodName + " Has not been added" + "<br/>";
					}
				}

				// Check if Session["tableNum"] already exists
				int ret_Code = -1;
				if (Session["tableNum"] == null)
				{
					if (conf_cart.Count > 0)
					{
						/*
						 * At least 1 item in cart
						 */
						ret_Code = ordersController.CheckoutCart(coupon_Code, conf_cart);

						// Write prices to Session
						Session["total_Price"] = final_Price.ToString();
						Session["discount"] = discount.ToString();
						Session["discounted_Price"] = disc_price_Rounded;

						// Write Table Number to Session
						Session["tableNum"] = tableNum;

						// Write Checked-out cart
						Session["checked_out_cart"] = conf_cart;
					}
					else
                    {
						lbl_Info.InnerText = "No items found in cart" + "\n";
                    }
				}
                else
                {
					ret_Code = 1;
                }
				//int ret_Code = 0;

				lbl_Debug_Output.Text += "[DEBUG - DATETIME] Start DateTime : " + start_Datetime + "<br/>";
				lbl_Debug_Output.Text += "[DEBUG - DATETIME] End   DateTime : " + end_Datetime + "<br/>";

				/* #DEBUG
				for (int i = 0; i < conf_cart.Count; i++)
				{
					List<String> tmp_Cart = conf_cart[i];

					for (int j = 0; j < tmp_Cart.Count; j++)
					{
						lbl_Debug_Output.Text += "[DEBUG] " + i.ToString() + " : " + tmp_Cart[j] + "<br/>";
					}
				}
				*/

				if (ret_Code == 1)
                {
					// Success
					Response.Redirect("~/PaymentSummary.aspx");
                }
                else
                {
					lbl_Info.InnerText += "Checking out of cart failed. \n";
                }
			}

			ViewAllInCart();

			// Display in Table
			lbl_Debug_Output.Text += table_Text.ToString() + "<br/>";
		}
		protected void btn_remove_from_Cart(object sender, EventArgs e)
        {
			//string prodID_to_remove = tb_prodID_to_remove.Value;
			Button btn_item_to_Remove = (Button)sender; // Declare and Initialize Button sender object
			string button_ROWID = btn_item_to_Remove.ID; // Get ID of the selected button
			// int ROWID_to_remove = tb_prodID_to_remove

			// Remove From Cart
			RemoveFromCartByPosition(int.Parse(button_ROWID));
        }
		protected void btn_clear_all_in_Cart(object sender, EventArgs e)
        {
			/*
			 * Clear all items in cart
			 */
			ClearCart();
        }
		protected void btn_go_to_Menu(object sender, EventArgs e)
		{
			/*
			 * Go To Menu
			 */
			Response.Redirect("~/Menu.aspx");
		}

		// Helper Functions
		public void ViewAllInCart()
		{
			/*
			 * Display all items added to cart
			 */
			List<List<String>> cart = (List<List<String>>)Session["cart"];
			List<List<String>> dataset = new List<List<String>>(); // Create master dataset for getting all rows to populate datagridview table
			int cart_size = 0;
			List<Button> all_buttons = new List<Button>();

			// Reset Table
			table_Shopping_Cart.Rows.Clear();

			// Null Check
			if (cart == null)
			{
				cart = new List<List<String>>();
			}
			else
			{
				cart_size = cart.Count;
			}
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			// Data Validation : Check if current cart has more than 1
			if (cart_size > 0)
			{
				/*
				 * Gather dataset
				 */
				for (int i = 0; i < cart_size; i++)
				{
					// Initialize Variables
					List<String> curr_row = cart[i]; // Create a new List to store current row					
					List<String> tmp = new List<String>(); // Create a new List to store data from the current row

					// Get Variables
					string productID = curr_row[0];
					string categoryID = curr_row[1];
					string productName = curr_row[2];
					string price = curr_row[3];
					string status = curr_row[4];
					Button new_button = new Button(); // Create a new button

					// Design new button
					new_button.ID = i.ToString();
					new_button.Attributes.Add("onserverclick", "btn_remove_from_Cart");
					new_button.Click += new EventHandler(btn_remove_from_Cart);
					new_button.Attributes.Add("runat", "server");
					new_button.Text = "Remove from Cart";
					lbl_Info.InnerText += "ID : " + i.ToString() + "\n";

					// Add every index as a new line in the StringBuilder / table entry
					tmp.Add((i + 1).ToString());
					tmp.Add(productID);
					tmp.Add(categoryID);
					tmp.Add(productName);
					tmp.Add(price);
					tmp.Add(status);

					// Add temporary dataset to list of rows
					dataset.Add(tmp);

					// Add newly created button to all_buttons
					all_buttons.Add(new_button);
				}

				/*
				 * Headers 
				 */
				// Create Table Header
				List<String> HeaderText = new List<String>() { "s/n", "Product ID", "Category ID", "Product Name", "Price", "Payment Status", "Button" };

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
				table_Shopping_Cart.Rows.Add(header);

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

					// Append Button to table row
					TableCell btn_cell = new TableCell(); // Create new cell for Button
					btn_cell.Controls.Add(all_buttons[i]); // Add Control to Cell
					new_tablerow.Cells.Add(btn_cell); // Add Button Cell to TableRow

					// Append Rows to table
					table_Shopping_Cart.Rows.Add(new_tablerow);
				}
			}
			else
			{
				// No Items Found
				TableRow curr_row = new TableRow();
				TableCell new_cell = new TableCell();
				new_cell.Text = "No Items in Cart. <br/>";
				curr_row.Cells.Add(new_cell);
				table_Shopping_Cart.Rows.Add(curr_row);
			}
		}
		public int RemoveFromCart(string prodID)
        {
			/*
			 * Remove Product ID from SESSION 'cart'
			 */
			List<List<String>> cart = (List<List<String>>)Session["cart"];
			List<List<String>> dataset = new List<List<String>>(); // Create master dataset for getting all rows to populate datagridview table
			int cart_size = 0;
			int res_Code = 1;

			// Reset Table
			table_Shopping_Cart.Rows.Clear();

			// Null Check
			if (cart == null)
			{
				cart = new List<List<String>>();
			}
			else
			{
				cart_size = cart.Count;
			}
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			if (cart_size > 0)
			{
				// Create Temporary cart for manipulation
				List<List<String>> cart_tmp = cart;

				// Get Initial Cart Size
				int old_cart_size = cart_tmp.Count;

				// Check for Index of selected product ID
				int pos = -1; // Position of selected product ID
				for(int i=0; i<cart_tmp.Count; i++)
                {
					List<String> curr_row = cart_tmp[i];

					string curr_prod_ID = curr_row[0]; // Get current row's Product ID
					if (curr_prod_ID == prodID)
					{
						pos = i;
						break;
					}
				}
				// Remove ROW from cart
				if (pos > -1)
				{
					// Check if cart is big
					cart_tmp.RemoveAt(pos);
				}
                else
                {
					lbl_Info.InnerText = "Product ID provided is not valid.";
                }

				// Get New Cart Size
				int new_cart_size = cart_tmp.Count;

				// If cart size is smaller than initial cart size, return true (0)
				if(new_cart_size == old_cart_size - 1)
                {
					res_Code = 0;
                }

				// Store new cart
				Session["cart"] = cart_tmp;
			}

			ViewAllInCart();

			return res_Code;
		}
		public int RemoveFromCartByPosition(int ROWID)
		{
			/*
			 * Remove item from Session["cart"] by row number
			 */
			List<List<String>> cart = (List<List<String>>)Session["cart"];
			List<List<String>> dataset = new List<List<String>>(); // Create master dataset for getting all rows to populate datagridview table
			int cart_size = 0;
			int res_Code = 1;

			// Reset Table
			table_Shopping_Cart.Rows.Clear();

			// Null Check
			if (cart == null)
			{
				cart = new List<List<String>>();
			}
			else
			{
				cart_size = cart.Count;
			}
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			if (cart_size > 0)
			{
				// Create Temporary cart for manipulation
				List<List<String>> cart_tmp = cart;

				// Get Initial Cart Size
				int old_cart_size = cart_tmp.Count;

				// Remove ROW from cart
				if (ROWID > -1)
				{
					// Check if cart is big
					cart_tmp.RemoveAt(ROWID);
				}
				else
				{
					lbl_Info.InnerText = "Product requested to remove is not valid.";
				}

				// Get New Cart Size
				int new_cart_size = cart_tmp.Count;

				// If cart size is smaller than initial cart size, return true (0)
				if (new_cart_size == old_cart_size - 1)
				{
					res_Code = 0;
				}

				// Store new cart
				Session["cart"] = cart_tmp;
			}

			ViewAllInCart();

			return res_Code;
		}
		public int ClearCart()
        {
			/*
			 * Clear all items in cart
			 */
			List<List<String>> cart = (List<List<String>>)Session["cart"];
			List<List<String>> dataset = new List<List<String>>(); // Create master dataset for getting all rows to populate datagridview table
			int cart_size = 0;
			int res_Code = 1;

			// Reset Table
			table_Shopping_Cart.Rows.Clear();

			// Null Check
			if (cart == null)
			{
				cart = new List<List<String>>();
			}
			else
			{
				cart_size = cart.Count;
			}
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			if (cart_size > 0)
			{
				// Reset cart
				Session["cart"] = new List<List<String>>();
			}

			ViewAllInCart();

			return res_Code;
		}
	}
}