using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveHead.Menu
{
    public partial class Menu : System.Web.UI.Page
    {
        ProductsController productsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            productsController = new ProductsController();
            StringBuilder html = new StringBuilder();

            List<Product> products = productsController.GetAllProducts();

            if (products == null)
            {
                PlaceHolder_Empty.Visible = true;
                return;
            }

            products.ForEach(product => {
                int productID = product.ProductID;
                string productName = product.ProductName;
                string image = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(product.Image));
                double price = product.Price;

                html.AppendLine("<article class='card product-item'>");
                html.AppendLine("<header class='card__header'>");
                html.AppendLine("<h1 class='product__title'>");
                html.AppendLine(productName);
                html.AppendLine("</h1>");
                html.AppendLine("</header>");
                html.AppendLine("<div class='card__image'>");
                html.AppendLine(string.Format("<img src='{0}' alt='{1}'>", image, productName));
                html.AppendLine("</div>");
                html.AppendLine("<div class='card__content'>");
                html.AppendLine("<h2 class='product__price'>$");
                html.AppendLine(string.Format("{0:0.00}", price));
                html.AppendLine("</h2>");
                html.AppendLine("</div>");
                html.AppendLine("<div class='card__actions'>");
                html.AppendLine(string.Format("<a href='AddCart.aspx?id={0}' target='_blank' class='btn-custom' onclick=\"window.open(this.href, 'Add to Cart', 'left=10,top=10,width=20,height=20,toolbar=1,resizable=0'); return false;\"'>Add to Cart</a>", productID));
                html.AppendLine("</div>");
                html.AppendLine("</article>");
            });

            list_Products.InnerHtml = html.ToString();
        }
    }
}