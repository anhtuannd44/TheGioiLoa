using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class BlogController : Controller
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly BlogService _blogService = new BlogService();
        private readonly HelperFunction _helper = new HelperFunction();
        // GET: Blog
        //public ActionResult BlogList(int? page)
        //{
        //    var blogList = _blogService.GetBlogList("Public", 1).OrderByDescending(a => a.DateCreated).ToPagedList(page ?? 1, 10);
        //    ViewBag.Title = "Tin Tức";
        //    return View(blogList);
        //}

        public ActionResult BlogList(int blogCategoryId, string url, int? page)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blogList = new List<BlogViewModel>();
            if (blogCategoryId == 0 && url == "tat-ca-danh-muc")
            {
                blogList = _blogService.GetBlogList("Public", 1);
                ViewBag.Title = "Tin Tức";
            }
            else
            {
                var blogCategory = db.BlogCategory.Find(blogCategoryId);
                if (blogCategory.Url.ToLower() != _helper.DeleteSpace(url.ToLower()))
                {
                    return HttpNotFound();
                }
                blogList = _blogService.GetBlogList("Public", 1).Where(a => a.BlogCategoryId == blogCategoryId).ToList();
                ViewBag.Title = blogCategory.Name;
            }
            var model = blogList.ToPagedList(page ?? 1, 10);
            return View(model);
        }
        public ActionResult BlogDetails(int blogId, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = db.Blog.Find(blogId);
            if (blog == null)
            {
                return HttpNotFound();
            }
            if (blog.Url.ToLower() != _helper.DeleteSpace(url.ToLower()))
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        public ActionResult CategorySidebar()
        {
            var model = db.BlogCategory.ToList();
            return PartialView("_CategorySidebarPartial", model);
        }
    }
}