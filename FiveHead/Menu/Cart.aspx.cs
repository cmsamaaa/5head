using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Text;

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
        }

        private void BindCart()
        {
            StringBuilder html = new StringBuilder();
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];

            if (cart == null)
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
                html.AppendLine(string.Format("<a href='RemoveCart.aspx?id={0}' class='btn-custom danger'>Remove</a>", product.ProductID));
                html.AppendLine("</li>");
            }

            list_Cart.InnerHtml = html.ToString();
        }
    }
}