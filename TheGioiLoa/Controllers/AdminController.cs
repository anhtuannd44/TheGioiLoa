using System;
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

            var parentList = db.Category.Where(a => a.CategoryParentId == null);
            ViewBag.CategoryParentId = new SelectList(parentList.ToList(), "CategoryId", "Name");
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

        //[HttpPost]
        //public ActionResult EditCategoryForm(EditCategoryViewModel category)
        //{
        //    category.Name = _helper.DeleteSpace(category.Name);
        //    var model = new CategoryViewModel();
        //    var editItem = db.Category.Find(category.CategoryId);
        //    if (editItem == null)
        //    {
        //        model.Notification = "error";
        //    }
        //    else if (ModelState.IsValid)
        //    {
        //        if (_categoryService.IsExistedCategory(category.Name))
        //        {
        //            model.Notification = "existed";
        //        }
        //        else
        //        {
        //            //_categoryService.CreateCategory(category);
        //            model.Notification = "successed";
        //        }
        //        model.CategoryList = _categoryService.GetAllCategories();
        //    }
        //    else
        //        model.Notification = "error";
        //    ViewBag.CategoryParentId = new SelectList(_categoryService.GetParentCategoryList(), "CategoryId", "Name");
        //    return PartialView("_CategoryListPartial", model);
        //}
    }
}