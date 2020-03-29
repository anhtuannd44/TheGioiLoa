using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;

namespace TheGioiLoa.Controllers
{
    public class PageController : Controller
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();

        // GET: Page
        public ActionResult PageDetails(int pageId, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var page = db.Blog.Find(pageId);
            if (page == null)
            {
                return HttpNotFound();
            }
            if (page.Url.ToLower() != _helper.DeleteSpace(url.ToLower()) || page.Type == 1 || page.Status != 1)
            {
                return HttpNotFound();
            }
            return View(page);
        }
    }
}