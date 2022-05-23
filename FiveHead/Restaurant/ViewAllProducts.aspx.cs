using FiveHead.Controller;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class ViewAllProducts : System.Web.UI.Page
    {
        ProductsController productsController;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            PlaceHolder_NoProduct.Visible = false;
            productsController = new ProductsController();
            DataSet ds;

            string search = Request.QueryString["search"];
            if (!string.IsNullOrEmpty(search))
                ds = productsController.SearchAllProducts(search);
            else
                ds = productsController.GetAllProductsDataSet();

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
                    dr["message"] = "return confirm('Are you sure you want to suspend the menu item?')";
                    dr["css"] = "btn btn-danger";
                    dr["editVisible"] = true;
                }
                else
                {
                    dr["suspend"] = "Re-activate";
                    dr["message"] = "return confirm('Are you sure you want to re-activate the menu item?')";
                    dr["css"] = "btn btn-success";
                    dr["editVisible"] = false;
                }
            }

            gv_Products.DataSource = ds;
            gv_Products.DataBind();

            if (ds.Tables[0].Rows.Count == 0)
                PlaceHolder_NoProduct.Visible = true;
        }

        protected void gv_Products_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Products.PageIndex = e.NewPageIndex;
            gv_Products.DataBind();
        }

        protected void gv_Products_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            productsController = new ProductsController();
            int index = Convert.ToInt32(e.CommandArgument) % gv_Products.PageSize;

            try
            {
                GridViewRow row = gv_Products.Rows[index];
            }
            catch (Exception)
            {
                index = 0;
            }

            Label productIDLabel = (Label)gv_Products.Rows[index].FindControl("lbl_ProductID");
            int productID = Convert.ToInt32(productIDLabel.Text);

            int result = 0;
            switch (e.CommandName)
            {
                case "Edit":
                    Session["edit_ProductID"] = productID;
                    Response.Redirect("EditProduct.aspx", true);
                    break;
                case "Suspend":
                    result = productsController.SuspendProduct(productID);
                    if (result == 1)
                        Response.Redirect("ViewAllProducts.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewAllProducts.aspx?suspend=false", true);
                    break;
                case "Re-activate":
                    result = productsController.ReactivateProduct(productID);
                    if (result == 1)
                        Response.Redirect("ViewAllProducts.aspx?activate=true", true);
                    else
                        Response.Redirect("ViewAllProducts.aspx?activate=false", true);
                    break;
                default:
                    break;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ViewAllProducts.aspx?search={0}", tb_Search.Value), true);
        }
    }
}