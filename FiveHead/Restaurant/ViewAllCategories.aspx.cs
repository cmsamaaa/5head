using FiveHead.Controller;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class ViewAllCategories : System.Web.UI.Page
    {
        CategoriesController categoriesController;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            categoriesController = new CategoriesController();
            DataSet ds = categoriesController.GetAllCategoriesDataSet();
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
                    dr["message"] = "return confirm('Are you sure you want to suspend the category?')";
                    dr["css"] = "btn btn-danger";
                    dr["editVisible"] = true;
                }
                else
                {
                    dr["suspend"] = "Re-activate";
                    dr["message"] = "return confirm('Are you sure you want to re-activate the category?')";
                    dr["css"] = "btn btn-success";
                    dr["editVisible"] = false;
                }
            }

            gv_Categories.DataSource = ds;
            gv_Categories.DataBind();
        }

        protected void gv_Categories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Categories.PageIndex = e.NewPageIndex;
            gv_Categories.DataBind();
        }

        protected void gv_Categories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            categoriesController = new CategoriesController();
            int index = Convert.ToInt32(e.CommandArgument) % gv_Categories.PageSize;

            try
            {
                GridViewRow row = gv_Categories.Rows[index];
            }
            catch (Exception)
            {
                index = 0;
            }

            Label categoryIDLabel = (Label)gv_Categories.Rows[index].FindControl("lbl_CategoryID");
            int categoryID = Convert.ToInt32(categoryIDLabel.Text);

            int result = 0;
            switch (e.CommandName)
            {
                case "Edit":
                    Session["edit_CategoryID"] = categoryID;
                    Response.Redirect("EditCategory.aspx", true);
                    break;
                case "Suspend":
                    result = categoriesController.SuspendCategory(categoryID);
                    if (result == 1)
                        Response.Redirect("ViewAllCategories.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewAllCategories.aspx?suspend=false", true);
                    break;
                case "Re-activate":
                    result = categoriesController.ReactivateCategory(categoryID);
                    if (result == 1)
                        Response.Redirect("ViewAllCategories.aspx?activate=true", true);
                    else
                        Response.Redirect("ViewAllCategories.aspx?activate=false", true);
                    break;
                default:
                    break;
            }
        }
    }
}