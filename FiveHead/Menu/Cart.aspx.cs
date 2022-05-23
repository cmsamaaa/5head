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
        CouponsController couponsController;

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
                PlaceHolder_Content.Visible = false;
                PlaceHolder_Empty.Visible = true;
                return;
            }

            double totalBill = 0.00;
            foreach (KeyValuePair<int, int> item in cart)
            {
                productsController = new ProductsController();
                Product product = productsController.GetProductByID(item.Key);

                html.AppendLine("<li class='cart__item'>");
                html.AppendLine(string.Format("<h2>{0}</h2>", product.ProductName));
                html.AppendLine(string.Format("<h4>${0:0.00}</h4>", product.Price));
                html.AppendLine(string.Format("<h4>Qty: {0}</h4>", item.Value));
                html.AppendLine("<input type='hidden' name='actionCommand' id='actionCommand' />");
                html.AppendLine("<input type='hidden' name='productID' id='productID' />");
                html.AppendLine(string.Format("<a href='#' class='btn-custom danger' onclick=\"doPostBack('remove', {0}); return false;\">Remove</a>", product.ProductID));
                html.AppendLine("</li>");

                totalBill += product.Price * item.Value;
            }

            list_Cart.InnerHtml = html.ToString();
            lbl_TotalBill.Text = string.Format("${0:0.00}", totalBill);
        }

        private double GetCartBill()
        {
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];

            if (cart == null || cart.Count == 0)
            {
                PlaceHolder_Content.Visible = false;
                PlaceHolder_Empty.Visible = true;
                return 0;
            }

            double totalBill = 0.00;
            foreach (KeyValuePair<int, int> item in cart)
            {
                productsController = new ProductsController();
                Product product = productsController.GetProductByID(item.Key);

                totalBill += product.Price * item.Value;
            }

            return totalBill;
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

        protected void btn_Coupon_Click(object sender, EventArgs e)
        {
            couponsController = new CouponsController();
            string code = tb_Coupon.Value + "";
             
            Coupon coupon = couponsController.GetCouponByCode(code.Trim());
            if (coupon == null)
                return;

            int discount = coupon.Discount;
            if (coupon.Deactivated)
                discount = 0;

            double totalBill = GetCartBill();
            double newBill = Math.Round(totalBill * (100 - discount) / 100, 2);

            if (discount > 0)
                lbl_TotalBill.Text = string.Format("${0:0.00} (-{1}%)", newBill, discount, code.Trim());
            else
                ShowMessage("The coupon code has already expired!");
        }

        private void ShowMessage(string Message)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("MyMessage"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message + "');", true);
            }
        }
    }
}