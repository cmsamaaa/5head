using FiveHead.Controller;
using System;

namespace FiveHead.Restaurant
{
    public partial class EditCategory : System.Web.UI.Page
    {
        CategoriesController categoriesController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GetInfo();
                }
                catch (Exception ex)
                {
                    Response.Redirect("ViewAllCategories.aspx", true);
                }
            }
        }

        private void GetInfo()
        {
            categoriesController = new CategoriesController();
            int categoryID = Convert.ToInt32(Session["edit_CategoryID"].ToString());
            tb_CategoryName.Value = categoriesController.GetCategoryNameByID(categoryID);
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_CategoryName.Value))
                Response.Redirect("EditCategory.aspx?error=empty");

            categoriesController = new CategoriesController();

            int categoryID = Convert.ToInt32(Session["edit_CategoryID"]);
            string categoryName = tb_CategoryName.Value;
            int result = categoriesController.UpdateCategoryName(categoryID, categoryName);
            if (result == 1)
                Response.Redirect("EditCategory.aspx?update=true", true);
            else
                Response.Redirect("EditCategory.aspx?update=false&cat=" + categoryName, true);
        }
    }
}