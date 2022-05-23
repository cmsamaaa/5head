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
        CouponsController couponsController;
        OrdersController ordersController;
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
                html.AppendLine(string.Format("<h2 style='width:10rem;'>{0}</h2>", product.ProductName));
                html.AppendLine(string.Format("<h4>${0:0.00}/qty</h4>", product.Price));
                html.AppendLine(string.Format("<h4>Qty: {0}</h4>", item.Value));
                html.AppendLine("<input type='hidden' name='actionCommand' id='actionCommand' />");
                html.AppendLine("<input type='hidden' name='productID' id='productID' />");
                html.AppendLine(string.Format("<a href='#' class='btn-custom danger' onclick=\"doPostBack('remove', {0}); return false;\"><i class=\"fa fa-trash\" aria-hidden=\"true\"></i></a>", product.ProductID));
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

        private double GetCartDiscountedBill(Coupon coupon)
        {
            if (coupon == null)
                return 0;

            int discount = coupon.Discount;
            if (coupon.Deactivated)
                discount = 0;

            double totalBill = GetCartBill();
            return Math.Round(totalBill * (100 - discount) / 100, 2);
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

        private void ShowMessage(string Message)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("MyMessage"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message + "');", true);
            }
        }

        protected void btn_Coupon_Click(object sender, EventArgs e)
        {
            couponsController = new CouponsController();
            string code = tb_Coupon.Value + "";
             
            Coupon coupon = couponsController.GetCouponByCode(code.Trim());
            double newBill = GetCartDiscountedBill(coupon);

            if (coupon == null)
                return;

            int discount = coupon.Discount;
            if (coupon.Deactivated)
                discount = 0;

            if (discount > 0)
                lbl_TotalBill.Text = string.Format("${0:0.00} (-{1}%)", newBill, discount, code.Trim());
            else
                ShowMessage("The coupon code has already expired!");
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            Session["cartSession"] = null;
            BindCart();
        }

        protected void btn_Order_Click(object sender, EventArgs e)
        {

            Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];
            List<Product> products = new List<Product>();
            bool sessionExist = int.TryParse(Session["tableNo"] + "", out int tableNo);

            if (cart == null || cart.Count == 0 || !sessionExist)
                return;

            ordersController = new OrdersController();
            int result = ordersController.ClearPendingOrders(tableNo);

            foreach (KeyValuePair<int, int> item in cart)
            {
                productsController = new ProductsController();
                Product product = productsController.GetProductByID(item.Key);
                products.Add(product);
            }

            string code = tb_Coupon.Value + "";
            double totalBill = 0.00;
            couponsController = new CouponsController();
            Coupon coupon = couponsController.GetCouponByCode(code.Trim());
            if (coupon == null)
            {
                code = "";
                totalBill = GetCartBill();
            }
            else
                totalBill = GetCartDiscountedBill(coupon);

            string contact = tb_Contact.Value + "";

            result = 0;
            foreach(Product product in products)
            {
                ordersController = new OrdersController();
                result += ordersController.CreateOrder(product, tableNo, cart[product.ProductID], totalBill, code, contact);
            }

            if (result == products.Count)
                Response.Redirect("Billing.aspx", true);
            else
                ShowMessage("Failed to place the order. Please find the staff for assistance!");
        }
    }
}