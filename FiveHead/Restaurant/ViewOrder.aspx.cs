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

            lbl_TableNo.Text = tableNo.ToString();
            DataSet ds = ordersController.GetActiveOrderDetails(tableNo);
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("message", typeof(string));

            foreach (DataRow dr in dt.Rows)
                dr["message"] = "return confirm('Are you sure you want to suspend the order item? This action cannot be reverted.')";

            gv_Orders.DataSource = ds;
            gv_Orders.DataBind();
        }

        private void bindTotalBill()
        {
            ordersController = new OrdersController();

            if (!int.TryParse(Session["view_TableNo"].ToString(), out int tableNo))
                Response.Redirect("ViewActiveOrders.aspx", true);
            
            lbl_TotalBill.Text = string.Format("{0:0.00}", ordersController.GetTotalBill(tableNo, "Active"));
        }

        protected void gv_Orders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Orders.PageIndex = e.NewPageIndex;
            gv_Orders.DataBind();
        }

        protected void gv_Orders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ordersController = new OrdersController();
            int index = Convert.ToInt32(e.CommandArgument) % gv_Orders.PageSize;

            try
            {
                GridViewRow row = gv_Orders.Rows[index];
            }
            catch (Exception)
            {
                index = 0;
            }

            Label orderIDLabel = (Label)gv_Orders.Rows[index].FindControl("lbl_OrderID");
            int orderID = Convert.ToInt32(orderIDLabel.Text);

            if (e.CommandName.Equals("Suspend"))
            {
                //int result = ordersController.SuspendOrder(tableNumber);
                //if (result >= 1)
                //    Response.Redirect("ViewActiveOrders.aspx?suspend=true", true);
                //else
                //    Response.Redirect("ViewActiveOrders.aspx?suspend=false", true);
                ShowMessage("This feature is under development");
            }
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

        private void ShowMessage(string Message)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("MyMessage"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message + "');", true);
            }
        }
    }
}