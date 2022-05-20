using FiveHead.Controller;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class ViewActiveOrders : System.Web.UI.Page
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
            DataSet ds = ordersController.GetAllActiveOrders();
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("message", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["message"] = "return confirm('Are you sure you want to suspend the order? This action cannot be reverted.')";
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

            int result = 0;
            switch (e.CommandName)
            {
                case "View":
                    Session["view_TableNo"] = tableNumber;
                    Response.Redirect("ViewOrder.aspx", true);
                    break;
                case "Suspend":
                    result = ordersController.SuspendOrder(tableNumber);
                    if (result > 0)
                        Response.Redirect("ViewActiveOrders.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewActiveOrders.aspx?suspend=false", true);
                    break;
                default:
                    break;
            }
        }
    }
}