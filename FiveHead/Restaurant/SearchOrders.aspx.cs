using FiveHead.Controller;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class SearchOrders : System.Web.UI.Page
    {
        OrdersController ordersController = new OrdersController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            int.TryParse(Request.QueryString["search"], out int tableNo);

            ordersController = new OrdersController();
            DataSet ds = ordersController.SearchOrders(tableNo);

            DataTable dt = ds.Tables[0];
            dt.Columns.Add("message", typeof(string));
            dt.Columns.Add("suspendVisible", typeof(bool));

            foreach (DataRow dr in dt.Rows)
            {
                dr["message"] = "return confirm('Are you sure you want to suspend the order? This action cannot be reverted.')";

                if (dr["orderStatus"].Equals("Completed"))
                    dr["suspendVisible"] = false;
                else
                    dr["suspendVisible"] = true;
            }

            gv_Orders.DataSource = ds;
            gv_Orders.DataBind();

            if (ds.Tables[0].Rows.Count == 0 && !string.IsNullOrEmpty(Request.QueryString["search"]))
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

            int result = 0;
            switch (e.CommandName)
            {
                case "View":
                    Session["view_TableNo"] = tableNumber;
                    Session["view_End_Datetime"] = end_datetime;
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

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_Search.Value, out int tableNo))
                Response.Redirect("SearchOrders.aspx?error=true", true);
            else
                Response.Redirect("SearchOrders.aspx?search=" + tableNo, true);
        }
    }
}