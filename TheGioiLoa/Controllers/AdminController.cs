using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class AdminController : Controller
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private CategoryService _categoryService = new CategoryService();
        private HelperFunction _helper = new HelperFunction();
        // GET: Admin
        public ActionResult Categories()
        {
            var model = new CategoryViewModel()
            {
                CategoryList = _categoryService.GetAllCategories()
            };
            ViewBag.CategoryParentId = new SelectList(_categoryService.GetParentCategoryList(), "CategoryId", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryViewModel category)
        {
            var model = new CategoryViewModel();
            category.Name = _helper.DeleteSpace(category.Name);
            if (ModelState.IsValid)
            {
                if (_categoryService.IsExistedCategory(category.Name))
                {
                    model.Notification = "existed";
                }
                else
                {
                    _categoryService.CreateCategory(category);
                    model.Notification = "successed";
                }
                model.CategoryList = _categoryService.GetAllCategories();
            }
            else
                model.Notification = "error";
            ViewBag.CategoryParentId = new SelectList(_categoryService.GetParentCategoryList(), "CategoryId", "Name");
            return PartialView("_CategoryListPartial", model);
        }

        [HttpPost]
        public ActionResult EditCategoryForm(EditCategoryViewModel category)
        {
            category.Name = _helper.DeleteSpace(category.Name);
            var model = new CategoryViewModel();
            var editItem = db.Category.Find(category.CategoryId);
            if (editItem == null)
            {
                model.Notification = "error";
            }
            else if (ModelState.IsValid)
            {
                if (_categoryService.IsExistedCategory(category.Name))
                {
                    model.Notification = "existed";
                }
                else
                {
                    //_categoryService.CreateCategory(category);
                    model.Notification = "successed";
                }
                model.CategoryList = _categoryService.GetAllCategories();
            }
            else
                model.Notification = "error";
            ViewBag.CategoryParentId = new SelectList(_categoryService.GetParentCategoryList(), "CategoryId", "Name");
            return PartialView("_CategoryListPartial", model);
        }
    }
}