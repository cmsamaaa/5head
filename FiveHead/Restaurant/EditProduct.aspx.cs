using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.IO;
using System.Web;
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
                catch (Exception)
                {
                    Response.Redirect("ViewAllCategories.aspx", true);
                }
            }

            if (IsPostBack && fileUpload_Image.PostedFile != null)
            {
                if (fileUpload_Image.PostedFile.FileName.Length > 0)
                {
                    byte[] bytes = GetImageBytes(fileUpload_Image.PostedFile);
                    Session["uploadedImage"] = Convert.ToBase64String(bytes);
                    img_ProductImage.ImageUrl = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bytes));
                    lbl_Preview.Visible = true;
                }
            }
        }

        private void SetMenuCategories()
        {
            categoriesController = new CategoriesController();
            categoriesController.GetAllCategories().ForEach(category =>
            {
                if (!category.Deactivated)
                    ddl_Category.Items.Add(new ListItem(category.CategoryName, category.CategoryID.ToString()));
            });
        }

        private void GetInfo()
        {
            productsController = new ProductsController();
            int productID = Convert.ToInt32(Session["edit_ProductID"].ToString());

            Product product = productsController.GetProductByID(productID);
            tb_ProductName.Value = product.ProductName;
            img_ProductImage.ImageUrl = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(product.Image));
            tb_Price.Value = string.Format("{0:0.00}", product.Price);
            ddl_Category.Value = product.CategoryID.ToString();
            Session["UploadedImage"] = Convert.ToBase64String(product.Image);
        }

        private byte[] GetImageBytes(HttpPostedFile postedFile)
        {
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            return br.ReadBytes((Int32)fs.Length);
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ProductName.Value) || string.IsNullOrEmpty(tb_Price.Value) || string.IsNullOrEmpty(ddl_Category.Value))
                Response.Redirect("EditProduct.aspx?error=empty", true);

            if (!double.TryParse(tb_Price.Value, out double price))
                Response.Redirect(string.Format("EditProduct.aspx?price=invalid"), true);

            if (string.IsNullOrEmpty(Session["edit_ProductID"] + ""))
                Response.Redirect("ViewAllProducts.aspx", true);

            int productID = Convert.ToInt32(Session["edit_ProductID"].ToString().Trim());
            byte[] productImg = Convert.FromBase64String(Session["UploadedImage"].ToString());
            string productName = tb_ProductName.Value;
            price = Convert.ToDouble(tb_Price.Value);
            int categoryID = Convert.ToInt32(ddl_Category.Value);

            productsController = new ProductsController();
            int result = productsController.UpdateProduct(productID, productName, productImg, price, categoryID);
            if (result == 1)
            {
                Session["UploadedImage"] = null;
                Response.Redirect("EditProduct.aspx?update=true", true);
            }
            else
                Response.Redirect(string.Format("EditProduct.aspx?update=false&prod={0}&price={1}&cat={2}", productName, price, categoryID), true);
        }
    }
}