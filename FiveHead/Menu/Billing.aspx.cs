using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveHead.Menu
{
    public partial class Billing : System.Web.UI.Page
    {
        CouponsController couponsController;
        OrdersController ordersController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            bool sessionExist = int.TryParse(Session["tableNo"] + "", out int tableNo);
            if (!sessionExist)
                Response.Redirect("EnterTable.aspx", true);

            list_Orders.InnerHtml = "";

            StringBuilder html = new StringBuilder();
            ordersController = new OrdersController();

            List<Order> orders = ordersController.GetPaymentBill(tableNo);
            if (orders == null)
                Response.Redirect("Cart.aspx", true);

            foreach(Order order in orders)
            {
                html.AppendLine("<li class='cart__item'>");
                html.AppendLine(string.Format("<h2>{0}</h2>", order.ProductName));
                html.AppendLine(string.Format("<h4>${0:0.00}/qty</h4>", order.Price));
                html.AppendLine(string.Format("<h4>Qty: {0}</h4>", order.ProductQty));
                html.AppendLine("</li>");
            }

            list_Orders.InnerHtml = html.ToString();

            if (string.IsNullOrEmpty(orders[0].CouponCode))
                lbl_TotalBill.Text = string.Format("${0:0.00}", orders[0].FinalPrice);
            else
            {
                couponsController = new CouponsController();
                Coupon coupon = couponsController.GetCouponByCode(orders[0].CouponCode.Trim());
                lbl_TotalBill.Text = string.Format("${0:0.00} (-{1}%)", orders[0].FinalPrice, coupon.Discount, orders[0].CouponCode.Trim());
            }
        }

        protected void btn_Payment_Click(object sender, EventArgs e)
        {
            bool sessionExist = int.TryParse(Session["tableNo"] + "", out int tableNo);
            if (!sessionExist)
                Response.Redirect("Cart.aspx", true);

            ordersController = new OrdersController();
            int result = ordersController.UpdatePayment(tableNo);

            if (result > 0)
            {
                Session["cartSession"] = null;
                Response.Redirect("PaymentSuccess.aspx", true);
            }
            else
                ShowMessage("Failed to make payment! Please approach the staff for assistance!");
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