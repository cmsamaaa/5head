using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FiveHead.Controller
{
    public class OrdersController
    {
		// Global Variables
		Order order = new Order();
        List<List<String>> cart = new List<List<String>>(); // Create a new List of type List<String>, to hold all items being added to shopping cart
		//float final_Price = 0.0f; // Temporary placeholder for calculating Final Price

		// Functions
		public void AddToCart(int productID, int categoryID, string productName, float price)
		{
			/*
			Add to a List for temporary holding of cart
			*/
			List<String> curr_item = new List<String>();

			// Append to temporary cart
			curr_item.Add(productID.ToString());
			curr_item.Add(categoryID.ToString());
			curr_item.Add(productName);
			curr_item.Add(price.ToString());
			curr_item.Add("Not Paid");

			// Append temporary cart (of type List<String>) to global variable cart (of type List<List<String>>) for use
			cart.Add(curr_item);
		}
		public int CheckoutCart(int orderID, int staffID)
		{
			/*
			 * Take all values in 'cart' and Insert into table 
			 */
			int ret_Code = 0;
			int cart_size = cart.Count;
			if (cart_size > 0)
			{
				// If at least 1 item is found
				for(int i=0; i < cart_size; i++)
                {
					List<String> curr_order = cart[i];

					int productID = int.Parse(curr_order[0]);	// Convert String to Integer
					int categoryID = int.Parse(curr_order[1]);	// Convert String to Integer
					string productName = curr_order[2];
					float price = float.Parse(curr_order[3]);	// Convert String to Float
					string status = curr_order[4];

					order.insert_Orders(orderID, staffID, productID, productName, categoryID, price);
                }

				// Set as Success
				ret_Code = 1;
			}

			return ret_Code;
		}
		public int ProcessPayment(int orderID, int staffID)
		{
			/*
			 * Make Payment
			 * 
			 * 1. Get all rows by customer
			 *		- Get all prices
			 * 2. Calculate Final Price
			 * 3. Request user to pay [final_price]
			 * 4. Confirm Payment is Made
			 * 5. Update status of all rows belonging to customer's [orderID] and [staffID] to 'Paid'
			 */
			int ret_Code = 0;

			return ret_Code;
		}
		public float CalculateFinalPrice(List<List<String>> cart)
        {
			/*
			 * Get all cart items and calculate all price
			 */
			float calc_Price = 0.0f;
			float final_Price = 0.0f;

			int cart_Size = cart.Count;

			if(cart_Size > 0)
            {
				// More than 1 items

				for(int i=0; i < cart_Size; i++)
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

		public List<List<String>> GetMenuItems()
        {
			return order.GetMenuItems();
        }
		public List<List<String>> GetCart()
        {
			return cart;
        }
	}
}