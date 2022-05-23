using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            
            if(IsPostBack)
            {
                string actionCommand = Request.Form["actionCommand"] + "";
                actionCommand = Regex.Replace(actionCommand, @"[^0-9a-zA-Z\._]", "");

                if (actionCommand.Equals("add"))
                    AddToCart();
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
                html.AppendLine("<input type='hidden' name='actionCommand' id='actionCommand' />");
                html.AppendLine("<input type='hidden' name='productID' id='productID' />");
                html.AppendLine(string.Format("<a href='#' class='btn-custom' onclick=\"doPostBack('add', {0}); return false;\">Add to Cart</a>", productID));
                html.AppendLine("</div>");
                html.AppendLine("</article>");
            });

            list_Products.InnerHtml = html.ToString();
        }

        private void AddToCart()
        {
            int productID = Convert.ToInt32(Regex.Replace(Request.Form["productID"], @"[^0-9a-zA-Z\._]", ""));
            if ((Dictionary<int, int>)Session["cartSession"] == null)
            {
                Dictionary<int, int> cart = new Dictionary<int, int>();
                cart.Add(productID, 1);
                Session["cartSession"] = cart;
            }
            else
            {
                Dictionary<int, int> cart = (Dictionary<int, int>)Session["cartSession"];
                if (cart.ContainsKey(productID))
                {
                    int value = cart[productID];
                    cart[productID] = value + 1;
                }
                else
                    cart.Add(productID, 1);

                Session["cartSession"] = cart;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            list_Products.InnerHtml = "";
            productsController = new ProductsController();
            StringBuilder html = new StringBuilder();

            string search = tb_Search.Value + "";
            List<Product> products = productsController.List_SearchAllProducts(search.Trim());

            PlaceHolder_Empty.Visible = false;
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
                html.AppendLine("<input type='hidden' name='actionCommand' id='actionCommand' />");
                html.AppendLine("<input type='hidden' name='productID' id='productID' />");
                html.AppendLine(string.Format("<a href='#' class='btn-custom' onclick=\"doPostBack('add', {0}); return false;\">Add to Cart</a>", productID));
                html.AppendLine("</div>");
                html.AppendLine("</article>");
            });

            list_Products.InnerHtml = html.ToString();
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            tb_Search.Value = "";
            PlaceHolder_Empty.Visible = false;
            BindProducts();
        }
    }
}