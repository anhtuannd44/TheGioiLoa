using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Service
{

    public interface ICategoryService
    {
        void CreateCategory(CreateCategoryViewModel category);
        string EditCategory(EditCategoryViewModel category);
        void AddProduct(CreateProductViewModel product, string categoryIdList);

        Category GetParentCategory(string categoryId);
        IQueryable<Category> GetParentCategoryList();
        List<Category> GetAllCategories();
    }


    public class CategoryService : ICategoryService
    {
        public TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();

        public void CreateCategory(CreateCategoryViewModel category)
        {
            category.Name = _helper.DeleteSpace(category.Name);
            var addItem =  new Category
            {
                Name = category.Name,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Url = _helper.CreateUrl(category.Name),
                CategoryParentId = category.CategoryParentId
            };
            db.Category.Add(addItem);
            db.SaveChanges();
        }

        public string EditCategory(EditCategoryViewModel category)
        {
            category.Name = _helper.DeleteSpace(category.Name);
            if (string.IsNullOrEmpty(category.Name))
                return "empty";
            else
            {
                try
                {
                    var editItem = db.Category.Find(category.CategoryId);

                    editItem.DateModified = DateTime.Now;
                    editItem.Name = category.Name;
                    editItem.Url = _helper.CreateUrl(category.Name);

                    db.Entry(editItem).State = EntityState.Modified;
                    db.SaveChanges();
                    return "successed";
                }
                catch
                {
                    return "editfaild";
                }
            }
        }

        public void AddProduct(CreateProductViewModel product, string categoryIdList)
        {
            product.Name = _helper.DeleteSpace(product.Name);
            var addProduct = new Product()
            {
                Name = product.Name,
                Url = _helper.CreateUrl(product.Name),
                BrandId = product.BrandId,
                Description = product.Description,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Price = product.Price,
                ListedPrice = product.ListedPrice,
                Status = product.Status
            };
            db.Product.Add(addProduct);
            db.SaveChanges();

            var productId = db.Product.Where(a => a.Name == product.Name).OrderByDescending(x => x.ProductId).Take(1).Single().ProductId;
            if (!string.IsNullOrEmpty(categoryIdList))
            {
                string[] categoryIdArray = categoryIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in categoryIdList)
                {
                    var addToCategory = new CategoryProducts()
                    {
                        ProductId = productId,
                        CategoryId = Convert.ToInt32(item)
                    };
                    db.CategoryProduct.Add(addToCategory);
                }
                db.SaveChanges();
            }
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