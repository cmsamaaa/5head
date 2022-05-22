using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Menu
{
    public partial class AddCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void AddToCart()
        {
            if (!int.TryParse(Request.QueryString["id"], out int productID))
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            if(Session["cartSession"] == null)
            {
                Dictionary<int, int> cart = new Dictionary<int, int>();
                cart.Add(productID, 1);
                Session["cartSession"] = cart;
            }
            else
            {
                Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];
                bool isExisting = false;

                foreach (KeyValuePair<int, int> item in cart)
                    isExisting = productID == item.Key ? true : false;

                if (isExisting)
                {
                    int value = cart[productID];
                    cart[productID] = value + 1;
                }
                else
                    cart.Add(productID, 1);
                
                Session["cartSession"] = cart;
            }

            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}