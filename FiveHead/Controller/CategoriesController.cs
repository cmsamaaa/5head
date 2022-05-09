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
            category = new Category(categoryName);
            return category.CreateCategory();
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

        public Category GetCategoryByID(int categoryID)
        {
            return category.GetCategoryByID(categoryID);
        }

        public string GetCategoryNameByID(int categoryID)
        {
            category = GetCategoryByID(categoryID);
            if (category == null)
                return null;
            return category.CategoryName;
        }

        public int GetCategoryIDByName(string categoryName)
        {
            category = category.GetCategoryByName(categoryName);
            if (category == null)
                return -1;
            return category.CategoryID;
        }

        public int UpdateCategoryName(int categoryID, string categoryName)
        {
            int result = GetCategoryIDByName(categoryName);
            if (result == -1)
            {
                category = new Category(categoryID, categoryName);
                return category.UpdateCategoryName();
            }
            return 0;
        }

        public int ReactivateCategory(int categoryID)
        {
            category = new Category(categoryID, false);
            return category.UpdateCategoryStatus();
        }

        public int SuspendCategory(int categoryID)
        {
            category = new Category(categoryID, true);
            return category.UpdateCategoryStatus();
        }
    }
}