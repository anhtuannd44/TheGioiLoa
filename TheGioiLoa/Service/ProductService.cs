using System;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;

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
        public void CreateCategory(string categoryName)
        { 
            Category categoryItem = new Category()
            {
                CategoryId = _helper.CreateUrl(categoryName),
                Name = categoryName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                CategoryParentId = null
            };
            db.Category.Add(categoryItem);
            db.SaveChanges();
        }

        
        public void EditCategory()
        {

        }
    }
}