using System;
using System.Collections.Generic;
using System.Linq;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Service
{
    
    public interface ICategoryService
    {
        Category GetParentCategory(string categoryId);
        IQueryable<Category> GetParentCategoryList();
        List<Category> GetAllCategories();
    }


    public class CategoryService : ICategoryService
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();

        public bool IsExistedCategory(string categoryName)
        {
            var categoryId = _helper.CreateUrl(categoryName);
            Category categoryItem = db.Category.Find(categoryId);
            if (categoryItem == null)
                return false;
            else
                return true;
        }

        //public void CreateCategory(CreateCategoryViewModel category)
        //{
        //    Category categoryItem = new Category()
        //    {
        //        Url = _helper.CreateUrl(category.Name),
        //        Name = category.Name,
        //        DateCreated = DateTime.Now,
        //        DateModified = DateTime.Now
        //    };
        //    if (!category.IsCategoryParent)
        //        categoryItem.CategoryParentId = category.CategoryParentId;
        //    else
        //        categoryItem.CategoryParentId = null;
        //    db.Category.Add(categoryItem);
        //    db.SaveChanges();
        //}
        public List<Category> GetAllCategories()
        {
            return db.Category.ToList();
        }

        public Category GetParentCategory(string categoryId)
        {
            var categoryParentId = db.Category.Find(categoryId).CategoryParentId;
            return db.Category.Find(categoryParentId);
        }
        public IQueryable<Category> GetParentCategoryList()
        {
            return db.Category.Where(a => a.CategoryParentId == null);
        }
        public IQueryable<Category> GetChldCategoryList(int categoryParentId)
        {
            return db.Category.Where(a => a.CategoryParentId == categoryParentId);
        }

      

        //public void EditCategory(EditCategoryViewModel category)
        //{
           
        //    Category categoryItem = new Category()
        //    {
        //        Url = _helper.CreateUrl(category.Name),
        //        Name = category.Name,
        //        DateModified = DateTime.Now
        //    };
        //    if (category.IsCategoryParentEdit)
        //        categoryItem.CategoryParentId = category.CategoryParentId;
        //    else
        //        categoryItem.CategoryParentId = null;
        //    db.Category.Add(categoryItem);
        //    db.SaveChanges();
        //}
    }
}