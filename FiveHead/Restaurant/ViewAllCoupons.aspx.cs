using FiveHead.Controller;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class ViewAllCoupons : System.Web.UI.Page
    {
        CouponsController couponsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            couponsController = new CouponsController();
            DataSet ds = couponsController.GetAllCouponsDataSet();
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("suspend", typeof(string));
            dt.Columns.Add("message", typeof(string));
            dt.Columns.Add("css", typeof(string));
            dt.Columns.Add("editVisible", typeof(Boolean));

            foreach (DataRow dr in dt.Rows)
            {
                bool deactivated = Convert.ToBoolean(dr["deactivated"]);
                if (!deactivated)
                {
                    dr["suspend"] = "Suspend";
                    dr["message"] = "return confirm('Are you sure you want to suspend the coupon?')";
                    dr["css"] = "btn btn-danger";
                    dr["editVisible"] = true;
                }
                else
                {
                    dr["suspend"] = "Re-activate";
                    dr["message"] = "return confirm('Are you sure you want to re-activate the coupon?')";
                    dr["css"] = "btn btn-success";
                    dr["editVisible"] = false;
                }
            }

            gv_Coupons.DataSource = ds;
            gv_Coupons.DataBind();
        }

        protected void gv_Coupons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Coupons.PageIndex = e.NewPageIndex;
            gv_Coupons.DataBind();
        }

        protected void gv_Coupons_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            couponsController = new CouponsController();
            int index = Convert.ToInt32(e.CommandArgument) % gv_Coupons.PageSize;

            try
            {
                GridViewRow row = gv_Coupons.Rows[index];
            }
            catch (Exception)
            {
                index = 0;
            }

            Label couponIDLabel = (Label)gv_Coupons.Rows[index].FindControl("lbl_CouponID");
            int couponID = Convert.ToInt32(couponIDLabel.Text);

            int result = 0;
            switch (e.CommandName)
            {
                case "Edit":
                    Session["edit_CouponID"] = couponID;
                    Response.Redirect("EditCoupon.aspx", true);
                    break;
                case "Suspend":
                    result = couponsController.SuspendCoupon(couponID);
                    if (result == 1)
                        Response.Redirect("ViewAllCoupons.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewAllCoupons.aspx?suspend=false", true);
                    break;
                case "Re-activate":
                    result = couponsController.ReactivateCoupon(couponID);
                    if (result == 1)
                        Response.Redirect("ViewAllCoupons.aspx?activate=true", true);
                    else
                        Response.Redirect("ViewAllCoupons.aspx?activate=false", true);
                    break;
                default:
                    break;
            }
        }
    }
}