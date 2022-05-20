using FiveHead.Controller;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class ViewAllOrders : System.Web.UI.Page
    {
        OrdersController ordersController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            ordersController = new OrdersController();
            DataSet ds = ordersController.GetAllOrders();
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("totalBill", typeof(double));
            dt.Columns.Add("message", typeof(string));
            dt.Columns.Add("suspendVisible", typeof(bool));

            ordersController = new OrdersController();
            foreach (DataRow dr in dt.Rows)
            {
                dr["message"] = "return confirm('Are you sure you want to suspend the order? This action cannot be reverted.')";

                if (dr["orderStatus"].Equals("Completed") || dr["orderStatus"].Equals("Suspended"))
                {
                    dr["totalBill"] = ordersController.GetTotalBill(Convert.ToInt32(dr["tableNumber"]), Convert.ToDateTime(dr["end_datetime"]));
                    dr["suspendVisible"] = false;
                }
                else
                {
                    dr["totalBill"] = ordersController.GetTotalBill(Convert.ToInt32(dr["tableNumber"]), "Active");
                    dr["suspendVisible"] = true;
                }
            }

            gv_Orders.DataSource = ds;
            gv_Orders.DataBind();

            if (ds.Tables[0].Rows.Count == 0)
                PlaceHolder_NoOrders.Visible = true;
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

            Label tableNumberLabel = (Label)gv_Orders.Rows[index].FindControl("lbl_TableNumber");
            int tableNumber = Convert.ToInt32(tableNumberLabel.Text);

            Label datetimeLabel = (Label)gv_Orders.Rows[index].FindControl("lbl_EndDateTime");
            DateTime end_datetime = Convert.ToDateTime(datetimeLabel.Text);

            Label orderStatusLabel = (Label)gv_Orders.Rows[index].FindControl("lbl_OrderStatus");
            string orderStatus = orderStatusLabel.Text;

            int result = 0;
            switch (e.CommandName)
            {
                case "View":
                    Session["view_TableNo"] = tableNumber;
                    if (orderStatus.Equals("Active"))
                        Session["view_End_Datetime"] = "Active";
                    else
                        Session["view_End_Datetime"] = end_datetime;
                    Session["order_GoBack"] = HttpContext.Current.Request.Url.AbsolutePath;
                    Response.Redirect("ViewOrder.aspx", true);
                    break;
                case "Suspend":
                    result = ordersController.SuspendOrder(tableNumber);
                    if (result > 0)
                        Response.Redirect("ViewAllOrders.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewAllOrders.aspx?suspend=false", true);
                    break;
                default:
                    break;
            }
        }
    }
}