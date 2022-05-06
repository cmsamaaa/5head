using FiveHead.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class CreateProduct : System.Web.UI.Page
    {
        CategoriesController categoriesController;
        ProductsController productsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenuCategories();
            }
        }

        private void SetMenuCategories()
        {
            categoriesController = new CategoriesController();
            categoriesController.GetAllCategories().ForEach(category => ddl_Category.Items.Add(new ListItem(category.CategoryName, category.CategoryID.ToString())));
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ProductName.Value) || string.IsNullOrEmpty(tb_Price.Value) || string.IsNullOrEmpty(ddl_Category.Value))
                Response.Redirect("CreateProduct.aspx?error=empty", true);

            string productName = tb_ProductName.Value;
            double price = Convert.ToDouble(tb_Price.Value);
            int categoryID = Convert.ToInt32(ddl_Category.Value);

            productsController = new ProductsController();
            int result = productsController.CreateProduct(productName, price, categoryID);
            if (result == 1)
                Response.Redirect("CreateProduct.aspx?create=true", true);
            else
                Response.Redirect(String.Format("CreateProduct.aspx?create=false&prod={0}&price={1}&cat={2}", productName, price, categoryID), true);
        }
    }
}