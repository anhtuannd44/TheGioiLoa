using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Models;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class BlogController : Controller
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly BlogService _blogService = new BlogService();
        // GET: Blog
        public ActionResult AllBlog()
        {
            var blogList = _blogService.GetBlogList("Public", 1);
            return View(blogList);
        }

        public ActionResult BlogListInCategory(int blogCategoryId)
        {
            var blogList = _blogService.GetBlogList("Public", 1).Where(a => a.BlogCategoryId == blogCategoryId);
            return View(blogList);
        }
        public ActionResult CategorySidebar()
        {
            var model = db.BlogCategory.ToList();
            return PartialView("_CategorySidebarPartial", model);
        }
    }
}