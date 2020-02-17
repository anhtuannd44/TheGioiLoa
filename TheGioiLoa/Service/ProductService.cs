using System;
using System.Collections.Generic;
using System.Linq;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Service
{
    public class ProductService
    {
        private readonly Product _product;
        private readonly TheGioiLoaModel db;
        public void CreateProduct(Product product)
        {

        }
    }
    
    public class CategoryService
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

        public void CreateCategory(CreateCategoryViewModel category)
        {
            Category categoryItem = new Category()
            {
                Url = _helper.CreateUrl(category.Name),
                Name = category.Name,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            if (!category.IsCategoryParent)
                categoryItem.CategoryParentId = category.CategoryParentId;
            else
                categoryItem.CategoryParentId = null;
            db.Category.Add(categoryItem);
            db.SaveChanges();
        }
        public List<CategoryListViewModel> GetAllCategories()
        {
            List<CategoryListViewModel> result = new List<CategoryListViewModel>();
            var categoryParentList = GetParentCategoryList().ToList();
            foreach (var item in categoryParentList)
            {
                var addParentItem = new CategoryListViewModel()
                { 
                    CategoryId = item.CategoryId,
                    Name = item.Name
                };
                List<CategoryListViewModel> categoryChildList = new List<CategoryListViewModel>();
                var childItemList = GetChldCategoryList(item.CategoryId).ToList();
                foreach (var childItem in childItemList)
                {
                    var addChildItem
                        = new CategoryListViewModel()
                    { 
                        CategoryId = childItem.CategoryId,
                        Name = childItem.Name,
                        CategoryParentId = childItem.CategoryParentId
                    };
                    categoryChildList.Add(addChildItem);
                }
                addParentItem.CategoryChildList = categoryChildList;
                result.Add(addParentItem);
            }
            return result;
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

      

        public void EditCategory(EditCategoryViewModel category)
        {
           
            Category categoryItem = new Category()
            {
                Url = _helper.CreateUrl(category.Name),
                Name = category.Name,
                DateModified = DateTime.Now
            };
            if (category.IsCategoryParentEdit)
                categoryItem.CategoryParentId = category.CategoryParentId;
            else
                categoryItem.CategoryParentId = null;
            db.Category.Add(categoryItem);
            db.SaveChanges();
        }
    }
}