using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class EditProduct : System.Web.UI.Page
    {
        CategoriesController categoriesController;
        ProductsController productsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenuCategories();

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

        private void SetMenuCategories()
        {
            categoriesController = new CategoriesController();
            categoriesController.GetAllCategories().ForEach(category => ddl_Category.Items.Add(new ListItem(category.CategoryName, category.CategoryID.ToString())));
        }

        private void GetInfo()
        {
            productsController = new ProductsController();
            int productID = Convert.ToInt32(Session["edit_ProductID"].ToString());

            Product product = productsController.GetProductByID(productID);
            tb_ProductName.Value = product.ProductName;
            tb_Price.Value = String.Format("{0:0.00}", product.Price);
            ddl_Category.Value = product.CategoryID.ToString();
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ProductName.Value) || string.IsNullOrEmpty(tb_Price.Value) || string.IsNullOrEmpty(ddl_Category.Value))
                Response.Redirect("EditProduct.aspx?error=empty", true);

            if (!double.TryParse(tb_Price.Value, out double price))
                Response.Redirect(string.Format("EditProduct.aspx?price=invalid"), true);

            int productID = Convert.ToInt32(Session["edit_ProductID"].ToString());
            string productName = tb_ProductName.Value;
            price = Convert.ToDouble(tb_Price.Value);
            int categoryID = Convert.ToInt32(ddl_Category.Value);

            productsController = new ProductsController();
            int result = productsController.UpdateProduct(productID, productName, price, categoryID);
            if (result == 1)
                Response.Redirect("EditProduct.aspx?update=true", true);
            else
                Response.Redirect(string.Format("EditProduct.aspx?update=false&prod={0}&price={1}&cat={2}", productName, price, categoryID), true);
        }
    }
}