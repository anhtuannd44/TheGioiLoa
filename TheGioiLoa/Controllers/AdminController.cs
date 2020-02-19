using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class AdminController : Controller
    {
        public ICategoryService CategoryService { get; set; }

        private TheGioiLoaModel db = new TheGioiLoaModel();
        private HelperFunction _helper = new HelperFunction();

        // GET: Admin
        public ActionResult Categories()
        {
            var parentList = db.Category.Where(a => a.CategoryParentId == null);
            ViewBag.CategoryParentList = parentList.ToList();
            return View();
        }
        public ActionResult LoadCategoryList()
        {
            var category = db.Category;
            var model = new CategoryViewModel()
            {
                CategoryList = category.ToList()
            };
            if (category != null)
                model.Notification = "nodata";

            ViewBag.CategoryParentList = db.Category.Where(a => a.CategoryParentId == null).ToList();
            return PartialView("_CategoryListPartial", model);
        }
        [HttpPost]
        public string CreateCategory(CreateCategoryViewModel category)
        {
            var model = new CategoryViewModel();
            category.Name = _helper.DeleteSpace(category.Name);
            if (ModelState.IsValid)
            {
                var itemCategory = new Category()
                {
                    Name = category.Name,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Url = _helper.CreateUrl(category.Name),
                    CategoryParentId = category.CategoryParentId
                };

                db.Category.Add(itemCategory);
                db.SaveChanges();
                model.Notification = "successed";
            }
            else
                model.Notification = "error";

            return model.Notification;
        }

        [HttpPost]
        public string EditCategory(EditCategoryViewModel category)
        {
            category.Name = _helper.DeleteSpace(category.Name);
            var model = new CategoryViewModel();
            if (string.IsNullOrEmpty(category.Name))
                model.Notification = "empty";
            else
            {
                try
                {
                    var editItem = db.Category.Find(category.CategoryId);

                    editItem.CategoryParentId = category.CategoryParentId;
                    editItem.DateModified = DateTime.Now;
                    editItem.Name = category.Name;
                    editItem.Url = _helper.CreateUrl(category.Name);

                    db.Entry(editItem).State = EntityState.Modified;
                    db.SaveChanges();
                    model.Notification = "successed";
                }
                catch
                {
                    model.Notification = "editfaild";
                }
            }
            return model.Notification;
        }

        [HttpPost]
        public PartialViewResult LoadEditPartial(int categoryId)
        {
            var editItem = db.Category.Find(categoryId);
            var model = new EditCategoryModalViewModel()
            {
                Name = editItem.Name,
                CategoryId = editItem.CategoryId,
                CategoryParentId = editItem.CategoryParentId
            };
            var childCategory = db.Category.Where(a => a.CategoryParentId == categoryId).Count();
            if (childCategory == 0)
            {
                model.ParentList = db.Category.Where(a => a.CategoryParentId == null && a.CategoryId != categoryId).ToList();
            }
            else if (childCategory != 0 && editItem.CategoryParentId == null)
                model.ParentList = null;
            return PartialView("_EditCategoryPartial", model);
        }

        public ActionResult ProductList()
        {
            var model = db.CategoryProduct.Include(a => a.Category).Include(a => a.Product);
            return View();
        }

        public ActionResult CreateProduct()
        {
            ViewBag.BrandId = db.Brand.ToList();
            ViewBag.CategoryId = db.Category.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(CreateProductViewModel product)
        {
            product.Name = _helper.DeleteSpace(product.Name);
            if (ModelState.IsValid)
            {
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
                var getCategoryList = Request["CategoryId"];
                if (!string.IsNullOrEmpty(getCategoryList))
                {
                    string[] categoryIdList = getCategoryList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
                return RedirectToAction("ProductList");
            }

            ViewBag.BrandId = db.Brand.ToList();
            ViewBag.CategoryId = db.Category.ToList();
            return View(product);
        }

    }
}
