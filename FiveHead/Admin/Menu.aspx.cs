using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FiveHead.Controller;
using System.Text;

namespace FiveHead.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Global Variables
        OrdersController ordersController = new OrdersController();         // For handling Orders (Customer)
        ProfilesController profilesController = new ProfilesController();	// For handling Profiles

        protected void Page_Load(object sender, EventArgs e)
        {
			ViewAllMenuItems();
        }

		// Button Event Functions
		protected void btn_add_to_Cart(object sender, EventArgs e)
		{
			// Add selected item to cart
			AddToCart();
		}

		// Helper Functions
		public void ViewAllMenuItems()
		{
			/*
			 * Display all items in products (menu)
			 */
			List<List<String>> menu_items = ordersController.GetMenuItems();
			int number_of_items = menu_items.Count;
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			// Append a Line to StringBuilder
			text_to_show.AppendLine("| s/n | Product ID | Category ID | Product Name | Price |");
			text_to_show.AppendLine("\n"); // Write Newline

			if (number_of_items > 0) // Data Validation : Check if current cart has more than 1
			{
				for (int i = 0; i < number_of_items; i++)
				{
					StringBuilder curr_line = new StringBuilder();
					List<String> curr_item = menu_items[i];
					string productID = curr_item[0];
					string productName = curr_item[1];
					string price = curr_item[2];
					string categoryID = curr_item[3];

					// Add every index as a new line in the StringBuilder / table entry
					text_to_show.Append("| ");
					text_to_show.Append((i + 1) + " | ");
					text_to_show.Append(categoryID + " | ");
					text_to_show.Append(productID + " | ");
					text_to_show.Append(productName + " | ");
					text_to_show.Append(price + " | ");
					text_to_show.Append("<button id='" + productID + "'>" +
							"Add To Cart" +
						"</button>" + "\n"
					);
					text_to_show.Append(" |");

					text_to_show.AppendLine("\n"); // Write Newline
				}
			}
			else
			{
				text_to_show.AppendLine("No Items in Shopping Cart.");
				text_to_show.AppendLine("\n"); // Write Newline
			}

			// Write to Widget
			table_MenuItems.Text = text_to_show.ToString(); // C# ASP.NET table (aka asp:Literal) uses .Text
		}

		void AddToCart()
		{
			/*
			Add to a List for temporary holding of cart
			*/
			int ROWID = 0;
			List<List<String>> menu_items = ordersController.GetMenuItems();
			int number_of_items = menu_items.Count;
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			// Get Selected Item (Row)
			List<String> selected_item = menu_items[ROWID];
			int productID = int.Parse(selected_item[0]);
			string productName = selected_item[1];
			float price = float.Parse(selected_item[2]);
			int categoryID = int.Parse(selected_item[3]);

			// Add to Cart
			ordersController.AddToCart(productID, categoryID, productName, price);
		}
	}
}