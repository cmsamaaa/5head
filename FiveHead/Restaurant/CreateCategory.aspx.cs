using FiveHead.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class CreateCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_CategoryName.Value))
                Response.Redirect("CreateCategory.aspx?error=empty");

            CategoriesController categoriesController = new CategoriesController();

            string categoryName = tb_CategoryName.Value;
            int result = categoriesController.CreateCategory(categoryName);
            if (result == 1)
                Response.Redirect("CreateCategory.aspx?create=true", true);
            else
                Response.Redirect("CreateCategory.aspx?create=false&cat=" + categoryName, true);
        }
    }
}