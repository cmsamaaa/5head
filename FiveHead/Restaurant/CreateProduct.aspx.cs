using FiveHead.Controller;
using System;
using System.Drawing;
using System.IO;
using System.Web;
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

        private byte[] GetImageBytes(HttpPostedFile postedFile)
        {
            Stream fs = postedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            return br.ReadBytes((Int32)fs.Length);
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ProductName.Value) || string.IsNullOrEmpty(tb_Price.Value) || string.IsNullOrEmpty(ddl_Category.Value))
                Response.Redirect(string.Format("CreateProduct.aspx?error=empty&prod={0}&price={1}&cat={2}", tb_ProductName.Value, tb_Price.Value, ddl_Category.Value), true);

            if (!double.TryParse(tb_Price.Value, out double price))
                Response.Redirect(string.Format("CreateProduct.aspx?price=invalid&prod={0}&cat={1}", tb_ProductName.Value, ddl_Category.Value), true);

            string productName = tb_ProductName.Value;
            byte[] productImg = Convert.FromBase64String(Session["UploadedImage"].ToString());
            price = Convert.ToDouble(tb_Price.Value);
            int categoryID = Convert.ToInt32(ddl_Category.Value);

            productsController = new ProductsController();
            int result = productsController.CreateProduct(productName, productImg, price, categoryID);
            if (result == 1)
            {
                Session["UploadedImage"] = null;
                Response.Redirect("CreateProduct.aspx?create=true", true);
            }
            else
                Response.Redirect(string.Format("CreateProduct.aspx?create=false&prod={0}&price={1}&cat={2}", productName, price, categoryID), true);
        }
    }
}