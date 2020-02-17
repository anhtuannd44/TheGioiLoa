using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
    }
}
