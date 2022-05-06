using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;
using System.Data;

namespace FiveHead.Controller
{
    public class CategoriesController
    {
        Category category = new Category();

        public int CreateCategory(string categoryName)
        {
            return category.CreateCategory(categoryName);
        }

        public DataSet GetAllCategoriesDataSet()
        {
            return category.GetAllCategories();
        }

        public List<Category> GetAllCategories()
        {
            DataSet ds = GetAllCategoriesDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToList<Category>();
            else
                return null;
        }
    }
}