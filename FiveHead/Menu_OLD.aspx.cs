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
    public partial class Menu_OLD : System.Web.UI.Page
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

			ViewAllMenuItems();
		}

		// Button Event Functions
		protected void btn_go_to_Cart(object sender, EventArgs e)
        {
			/*
			 * Redirect to Cart.aspx
			 */
			Response.Redirect("~/Cart.aspx");
        }
		protected void btn_add_to_Cart(object sender, EventArgs e)
		{
			// Get ProductID of the selected product
			Button btn_selected_Cart = (Button)sender; // Declare and Initialize Button sender object
			string button_ID = btn_selected_Cart.ID; // Get ID of the selected button

			lbl_Debug_Output.Text = "Button ID : " + button_ID + "<br/>";

			// Add selected item to cart
			int ret_Code = AddToCart(button_ID);

			/*
			 * #DEBUG
			 */
			System.Diagnostics.Debug.WriteLine("Return Code : " + ret_Code.ToString());

			if(ret_Code == 0)
            {
				lbl_Debug_Output.Text += "Addition of Button ID [" + button_ID + "]: " + "Success" + "<br/>";

				// Entirely for DEBUG purposes
				List<List<String>> cart = (List<List<String>>)this.Session["cart"];

				lbl_Debug_Output.Text += "[DEBUG: Current Cart]" + "<br/>";
				for(int i=0; i < cart.Count; i++)
                {
					List<String> curr_item = cart[i];

					lbl_Debug_Output.Text += i.ToString();
					for(int j=0; j < curr_item.Count; j++)
                    {
						lbl_Debug_Output.Text += " | " + curr_item[j];
                    }
					lbl_Debug_Output.Text += "<br/>";
                }
			}
            else
            {
				lbl_Debug_Output.Text += "Addition of Button ID [" + button_ID + "]: " + "Error";
			}
			lbl_Debug_Output.Text += "<br/>";

			ViewAllMenuItems();
		}
		protected void btn_filter_menu_Items(object sender, EventArgs e)
        {
			// Variables
			List<String> menu_item_filtered = new List<String>();

			// Get Input text
			string filter_type = ddl_filter_Type.SelectedValue;
			string filter_keyword = tb_filter_Text.Value;

			// Check Filter Type
			switch(filter_type)
            {
				case "ProductName":
					// Get products that matches the filter (WHERE condition)
					// Default: By Product Name
					menu_item_filtered = ordersController.GetMenuItemByName(filter_keyword);
					break;
				case "ProductID":
					// Get products that matches the filter (WHERE condition)
					// Default: By Product Name
					menu_item_filtered = ordersController.GetMenuItemByProductID(filter_keyword);
					break;
			}

			// Populate 
			ViewFilteredMenuItems(menu_item_filtered);
        }
		protected void btn_view_all_menuItems(object sender, EventArgs e)
        {
			ViewAllMenuItems();
        }

		// Helper Functions
		public void ViewAllMenuItems()
		{
			/*
			 * Display all items in products (menu)
			 */
			List<List<String>> menu_items = ordersController.GetMenuItems();
			int number_of_items = menu_items.Count;
			List<List<String>> dataset = new List<List<String>>(); // Create master dataset for getting all rows to populate datagridview table
			List<Button> all_buttons = new List<Button>();

			// Reset Table
			table_MenuItems.Rows.Clear();

			// Data Validation : Check if current cart has more than 1
			if (number_of_items > 0)
			{
				/*
				 * Gather dataset
				 */
				for (int i = 0; i < number_of_items; i++)
				{
					// Initialize Variables
					List<String> curr_row = menu_items[i]; // Create a new List to store current row					
					List<String> tmp = new List<String>(); // Create a new List to store data from the current row
					Button new_button = new Button(); // Create a new button

					// Get Variables
					string productID = curr_row[0];
					string productName = curr_row[1];
					string price = curr_row[2];
					string categoryID = curr_row[3];

					// Design new button
					new_button.ID = productID;
					new_button.Attributes.Add("onserverclick", "btn_add_to_Cart");
					new_button.Click += new EventHandler(btn_add_to_Cart);
					new_button.Attributes.Add("runat", "server");
					new_button.Text = "Add to Cart";

					// Add every index as a new line in the StringBuilder / table entry
					tmp.Add((i + 1).ToString());
					tmp.Add(productID);
					tmp.Add(categoryID);
					tmp.Add(productName);
					tmp.Add(price);

					// Add temporary dataset to list of rows
					dataset.Add(tmp);

					// Add newly created button to all_buttons
					all_buttons.Add(new_button);
				}

				/*
				 * Headers 
				 */
				// Create Table Header
				List<String> HeaderText = new List<String>() { "s/n", "Product ID", "Category ID", "Product Name", "Price", "Button" };

				// Create new Table Header Row
				TableHeaderRow header = new TableHeaderRow();
				for(int i=0; i < HeaderText.Count; i++)
                {
					// Create new cell
					TableCell curr_header = new TableCell();

					// Populate Cell
					curr_header.Text = HeaderText[i];

					// Add Cell to Header Row
					header.Cells.Add(curr_header);
                }

				// Add Header Row to Table
				table_MenuItems.Rows.Add(header);

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
					//lbl_Debug_Output.Text += "ID: " + all_buttons[i].ID + "<br/>"; // DEBUG
					btn_cell.Controls.Add(all_buttons[i]); // Add Control to Cell
					new_tablerow.Cells.Add(btn_cell); // Add Button Cell to TableRow

					// Append Rows to table
					table_MenuItems.Rows.Add(new_tablerow);
				}
			}
			else
			{
				// No Items Found
				TableRow curr_row = new TableRow();
				TableCell new_cell = new TableCell();
				new_cell.Text = "No Items in Menu. <br/>";
				curr_row.Cells.Add(new_cell);
				table_MenuItems.Rows.Add(curr_row);
			}
		}
		public void ViewFilteredMenuItems(List<String> menu_items)
		{
			/*
			 * Display all items in products (menu)
			 */
			int number_of_items = menu_items.Count;
			List<String> dataset = new List<String>(); // Create master dataset for getting all rows to populate datagridview table
			Button btn_AddToCart = new Button();

			// Reset Table
			table_MenuItems.Rows.Clear();

			// Data Validation : Check if current cart has more than 1
			if (number_of_items > 0)
			{
				/*
				 * Gather dataset
				 */
				// Get Variables
				string productID = menu_items[0];
				string productName = menu_items[1];
				string price = menu_items[2];
				string categoryID = menu_items[3];
				Button new_button = new Button(); // Create a new button
				
				// Design new button
				new_button.ID = productID;
				new_button.Attributes.Add("onserverclick", "btn_add_to_Cart");
				new_button.Click += new EventHandler(btn_add_to_Cart);
				new_button.Attributes.Add("runat", "server");
				new_button.Text = "Add to Cart";

				// Add every index as a new line in the StringBuilder / table entry
				dataset.Add(productID);
				dataset.Add(categoryID);
				dataset.Add(productName);
				dataset.Add(price);

				// Add newly created button to all_buttons
				btn_AddToCart = new_button;

				/*
				 * Headers 
				 */
				// Create Table Header
				List<String> HeaderText = new List<String>() { "Product ID", "Category ID", "Product Name", "Price" };

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
				table_MenuItems.Rows.Add(header);

				/*
				 * Rows 
				 */
				// Create Table Rows
				TableRow new_tablerow = new TableRow();
				for (int i = 0; i < dataset.Count; i++)
				{
					/* 
					 * Get Current Row in Table Dataset
					 */
					String curr_row = dataset[i];

					/*
					 * Get Cells in current row
					 */
					TableCell new_cell = new TableCell();

					//table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
					new_cell.Text = curr_row;

					// Add Cell to row
					new_tablerow.Cells.Add(new_cell);
				}

				/*
				 * Buttons
				 */
				// Append Button to table row
				TableCell btn_cell = new TableCell(); // Create new cell for Button													 
				btn_cell.Controls.Add(btn_AddToCart); // Add Control to Cell

				// Add Button Cell to TableRow
				new_tablerow.Cells.Add(btn_cell); 

				// Append Rows to table
				table_MenuItems.Rows.Add(new_tablerow);
			}
			else
			{
				//// No Items Found
				//TableRow curr_row = new TableRow();
				//TableCell new_cell = new TableCell();
				//new_cell.Text = "No Items in Menu. <br/>";
				//curr_row.Cells.Add(new_cell);
				//table_MenuItems.Rows.Add(curr_row);
				ViewAllMenuItems();
			}
		}

		public int AddToCart(string productID)
		{
			/*
			Add to a List for temporary holding of cart
			*/

			// Variables
			int ret_Code = 1;
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			// Get Selected Item's ProductID

			// Get Product Details (by ProductID)
			List<String> product_details = ordersController.GetMenuItemByProductID(productID);
			if(product_details.Count > 0)
            {
				/*
				 * Check that there are items returned
				 */
				string productName = product_details[1];
				string price = product_details[2];
				string categoryID = product_details[3];

				// Create Temporary cart to retrieve SESSION 'cart'
				List<List<String>> tmp_Cart;
				if (this.Session["cart"] == null)
				{
					System.Diagnostics.Debug.WriteLine("SESSION is null");
					tmp_Cart = new List<List<String>>();
				}
				else
				{
					tmp_Cart = (List<List<String>>)this.Session["cart"];
				}

				// Get cart size before add
				int pre_cart_size = tmp_Cart.Count;

				// Add new entry
				List<String> new_Entry = new List<String>(); // List Container for New Entry
				new_Entry.Add(productID.ToString());
				new_Entry.Add(categoryID.ToString());
				new_Entry.Add(productName);
				new_Entry.Add(price.ToString());
				new_Entry.Add("Not Paid");

				// Append new entry to cart
				tmp_Cart.Add(new_Entry);

				// Get Cart Size after add
				int post_cart_size = tmp_Cart.Count;

				// Write to SESSION
				this.Session["cart"] = tmp_Cart;

				// Get Current DateTime
				DateTime now = DateTime.Now;
				string start_datetime = now.ToString("yyyy-MM-dd H:mm:ss");
				this.Session["start_datetime"] = start_datetime;
				
				// Data Validation
				if (post_cart_size == pre_cart_size + 1)
				{
					ret_Code = 0; // Success
				}

				ViewAllMenuItems();
			}

			return ret_Code;
		}
	}
}