using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class HomeController : Controller
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly InformationService _informationService = new InformationService();
        public ActionResult Index()
        {
            ViewBag.IsMenuExpand = true;
            return View();
        }
        public ActionResult MenuCategory(bool isMenuExpand)
        {
            var model = db.Category.ToList();
            ViewBag.IsMenuExpand = isMenuExpand;
            return PartialView("_MenuCategoryPartial", model);
        }
        public ActionResult MenuTop()
        {
            var model = _informationService.GetMenuList(0);
            return PartialView("_MenuTopPartial", model);
        }
        public ActionResult FooterMiddlePartial()
        {
            var model = new FooterViewModel
            {
                Footer1 = _informationService.GetMenuList(1),
                Footer2 = _informationService.GetMenuList(2),
                Footer3 = _informationService.GetMenuList(3)
            };
            return PartialView("_FooterMiddle", model);
        }
        public ActionResult LoadSocial(string social)
        {
            var model = _informationService.GetSocialLink(social);
            if (social == "Facebook")
                return PartialView("Social/_FacebookPartial", model);
            return PartialView("Social/_YoutubePartial", model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}