using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FiveHead.Menu
{
    public partial class Cart : System.Web.UI.Page
    {
        ProductsController productsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }

            if (IsPostBack)
            {
                string actionCommand = Request.Form["actionCommand"] + "";
                actionCommand = Regex.Replace(actionCommand, @"[^0-9a-zA-Z\._]", "");
                if (actionCommand.Equals("remove"))
                    RemoveFromCart();
            }
        }

        private void BindCart()
        {
            list_Cart.InnerHtml = "";

            StringBuilder html = new StringBuilder();
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];

            if (cart == null || cart.Count == 0)
            {
                btn_Order.Visible = false;
                PlaceHolder_Empty.Visible = true;
                return;
            }

            foreach (KeyValuePair<int, int> item in cart)
            {
                productsController = new ProductsController();
                Product product = productsController.GetProductByID(item.Key);

                html.AppendLine("<li class='cart__item'>");
                html.AppendLine(string.Format("<h1>{0}</h1>", product.ProductName));
                html.AppendLine(string.Format("<h2>Quantity: {0}</h2>", item.Value));
                html.AppendLine("<input type='hidden' name='actionCommand' id='actionCommand' />");
                html.AppendLine("<input type='hidden' name='productID' id='productID' />");
                html.AppendLine(string.Format("<a href='#' class='btn-custom danger' onclick=\"doPostBack('remove', {0}); return false;\">Remove</a>", product.ProductID));
                html.AppendLine("</li>");
            }

            list_Cart.InnerHtml = html.ToString();
        }

        private void RemoveFromCart()
        {
            if ((Dictionary<int, int>)Session["cartSession"] == null)
                return;

            int productID = Convert.ToInt32(Regex.Replace(Request.Form["productID"], @"[^0-9a-zA-Z\._]", ""));
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];

            if (!cart.ContainsKey(productID))
                return;

            int value = cart[productID];
            if (value == 1)
                cart.Remove(productID);
            else
                cart[productID] = value - 1;

            Session["cartSession"] = cart;

            BindCart();
        }
    }
}