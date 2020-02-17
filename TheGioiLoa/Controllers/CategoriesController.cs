using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TheGioiLoa.Models;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class CategoriesController : Controller
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private CategoryService _categoryService = new CategoryService();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(string categoryName)
        //{
        //    if (!string.IsNullOrEmpty(categoryName))
        //    {
        //        if (!_categoryService.IsExistedCategory(categoryName))
        //        {
        //            _categoryService.CreateCategory(categoryName);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMessage = "Chuyên mục đã tồn tại!";
        //        }
        //    }
        //    else
        //        ViewBag.ErrorMessage = "Bạn chưa nhập tên chuyên mục";
        //    return View();
        //}

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,CategoryParentId,DateCreated,DateModified")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
