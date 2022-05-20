using FiveHead.Controller;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class ViewOrder : System.Web.UI.Page
    {
        OrdersController ordersController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    bindGridView();
                    bindTotalBill();
                }
                catch (Exception)
                {
                    Response.Redirect("ViewActiveOrders.aspx", true);
                }
            }
        }

        private void bindGridView()
        {
            ordersController = new OrdersController();

            if (!int.TryParse(Session["view_TableNo"].ToString(), out int tableNo))
                Response.Redirect("ViewActiveOrders.aspx", true);

            DataSet ds;
            DateTime end_datetime;
            if (!Session["view_End_Datetime"].Equals("Active"))
            {
                DateTime.TryParse(Session["view_End_Datetime"].ToString(), out end_datetime);
                ds = ordersController.GetOrderDetails(tableNo, end_datetime);
                lbl_StartDatetime.Text = end_datetime.ToString();
            }
            else
            {
                ds = ordersController.GetActiveOrderDetails(tableNo);
                lbl_StartDatetime.Text = DateTime.Now.ToString();
            }

            DataTable dt = ds.Tables[0];
            dt.Columns.Add("message", typeof(string));

            foreach (DataRow dr in dt.Rows)
                dr["message"] = "return confirm('Are you sure you want to suspend the order item? This action cannot be reverted.')";

            if (dt.Rows[0]["orderStatus"].Equals("Completed") || dt.Rows[0]["orderStatus"].Equals("Suspended"))
                btn_Complete.Visible = false;

            gv_Orders.DataSource = ds;
            gv_Orders.DataBind();

            lbl_TableNo.Text = tableNo.ToString();
        }

        private void bindTotalBill()
        {
            ordersController = new OrdersController();

            if (!int.TryParse(Session["view_TableNo"].ToString(), out int tableNo))
                Response.Redirect("ViewActiveOrders.aspx", true);

            DateTime.TryParse(Session["view_End_Datetime"].ToString(), out DateTime end_datetime);

            if (Session["view_End_Datetime"].Equals("Active"))
                lbl_TotalBill.Text = string.Format("{0:0.00}", ordersController.GetTotalBill(tableNo, "Active"));
            else
                lbl_TotalBill.Text = string.Format("{0:0.00}", ordersController.GetTotalBill(tableNo, end_datetime));
        }

        protected void gv_Orders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Orders.PageIndex = e.NewPageIndex;
            gv_Orders.DataBind();
        }

        protected void btn_Complete_Click(object sender, EventArgs e)
        {
            ordersController = new OrdersController();

            if (!int.TryParse(Session["view_TableNo"].ToString(), out int tableNo))
                Response.Redirect("ViewActiveOrders.aspx", true);

            int result = ordersController.CompleteOrder(tableNo);

            if (result > 0)
                Response.Redirect("ViewActiveOrders.aspx?completed=true&tableNo=" + tableNo, true);
            else
                Response.Redirect("ViewActiveOrders.aspx?completed=false&tableNo=" + tableNo, true);
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["order_GoBack"].ToString(), true);
        }
    }
}