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
			int orderID = 1;
			int staffID = 1;
			int ret_Code = ordersController.CheckoutCart(orderID, staffID);

			ViewAllInCart();

			// Get Final Price
			float final_Price = ordersController.CalculateFinalPrice(ordersController.GetCart());
			StringBuilder table_Text = new StringBuilder();
			table_Text.AppendLine("Final Price: " + final_Price.ToString());

			// Display in Table
			table_Shopping_Cart.Text = table_Text.ToString();
		}

		protected void btn_make_Payment(object sender, EventArgs e)
        {
			/*
			 * When Button "btn_make_Payment" is clicked
			 */
			int orderID = 1;
			int staffID = 1;
			int ret_Code = MakePayment();
        }

		// Helper Functions
		public void ViewAllInCart()
		{
			/*
			 * Display all items added to cart
			 */
			int cart_size = ordersController.GetCart().Count;
			StringBuilder text_to_show = new StringBuilder(); // Use StringBuilder to append and get all strings to display in Shopping Cart ListView

			if (cart_size > 0)
			{
				// Append a Line to StringBuilder
				text_to_show.AppendLine("s/n | Product ID | Category ID | Product Name | Price | Status");
				text_to_show.AppendLine("\n"); // Write Newline

				for (int i = 0; i < cart_size; i++)
				{
					List<String> cart_contents = ordersController.GetCart()[i];

					// Get Columns
					string productID = cart_contents[0];
					string productName = cart_contents[1];
					string price = cart_contents[2];
					string categoryID = cart_contents[3];

					// Add every index as a new line in the StringBuilder / table entry
					text_to_show.AppendLine(
						i + " | " +
						categoryID + " | " +
						productID + " | " +
						productName + " | " +
						price + " | " +
						"<button id='" + productID + "'>" +
							"Add To Cart" +
						"</button>" + "\n"
					);

					// Write NewLine
					text_to_show.AppendLine("\n");
				}
			}
			else
			{
				text_to_show.AppendLine("No Items in Shopping Cart.");
				text_to_show.AppendLine("\n"); // Write Newline
			}

			// Write to Widget
			table_Shopping_Cart.Text = text_to_show.ToString(); // C# ASP.NET table (aka asp:Literal) uses .Text
		}

		public int MakePayment()
        {
			/*
			 * User to Make Payment
			 */
			int orderID = 1;
			int staffID = 1;
			int ret_Code = ordersController.ProcessPayment(orderID, staffID);

			return ret_Code;
        }
	}
}