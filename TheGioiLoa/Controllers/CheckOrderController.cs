using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Controllers
{
    public class CheckOrderController : Controller
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        // GET: CheckOrder
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CheckOrderDetails(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return RedirectToAction("Index");
            }
            var model = db.Order.Find(orderId);
            return View(model);
        }
    }
}