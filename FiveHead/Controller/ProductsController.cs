using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;
using System.Data;

namespace FiveHead.Controller
{
    public class ProductsController
    {
        Product product = new Product();

        public int CreateProduct(string productName, double price, int categoryID)
        {
            product = new Product(productName, price, categoryID);
            return product.CreateProduct(product);
        }

        public DataSet GetAllProductsDataSet()
        {
            return product.GetAllProducts();
        }

        public List<Product> GetAllProducts()
        {
            DataSet ds = GetAllProductsDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToList<Product>();
            else
                return null;
        }

        public Product GetProductByID(int productID)
        {
            return product.GetProductByID(productID);
        }

        public int UpdateProduct(int productID, string productName, double price, int categoryID)
        {
            Product product = new Product(productID, productName, price, categoryID);
            return product.UpdateProduct(product);
        }

        public int ReactivateProduct(int productID)
        {
            return product.UpdateProductStatus(productID, false);
        }

        public int SuspendProduct(int productID)
        {
            return product.UpdateProductStatus(productID, true);
        }
    }
}