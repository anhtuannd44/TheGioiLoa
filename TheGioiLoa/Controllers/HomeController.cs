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
        private readonly BlogService _blogService = new BlogService();
        private readonly ProductService _productService = new ProductService();

        public ActionResult Index()
        {
            ViewBag.IsMenuExpand = true;
            return View();
        }
        public ActionResult MenuCategory(bool isMenuExpand)
        {
            var model = db.Category.ToList();
            ViewBag.IsMenuExpand = isMenuExpand;
            return PartialView("Header/_MenuCategoryPartial", model);
        }
        public ActionResult MenuTop()
        {
            var model = _informationService.GetMenuList(0);
            return PartialView("Header/_MenuTopPartial", model);
        }
        public ActionResult FooterMiddlePartial()
        {
            var model = new FooterViewModel
            {
                Footer1 = _informationService.GetMenuList(1),
                Footer2 = _informationService.GetMenuList(2),
                Footer3 = _informationService.GetMenuList(3)
            };
            return PartialView("Footer/_FooterMiddle", model);
        }
        public ActionResult LoadSocial(string social)
        {
            var model = _informationService.GetSocialLink(social);
            if (social == "Facebook")
                return PartialView("Social/_FacebookPartial", model);
            if (social == "Youtube")
                return PartialView("Social/_YoutubePartial", model);
            return PartialView("Social/_ZaloPartial", model);
        }
        public ActionResult LoadSlider()
        {
            var model = _informationService.GetSliderImageList();
            return PartialView("Slider/_SliderPartial",model);
        }
        public ActionResult GetBlogHome()
        {
            var model = _blogService.GetBlogList("Public", 1).OrderByDescending(a=>a.DateCreated).Take(7);
            return PartialView("_BlogHomePartial", model);
        }
        public ActionResult HeaderContact()
        {
            var model = _informationService.GetContact();
            return PartialView("Header/_HeaderContact", model);
        }
        public ActionResult FooterContact()
        {
            var model = _informationService.GetFooterContact();
            return PartialView("Footer/_FooterContactPartial", model);
        }
        public ActionResult FooterCopyright()
        {
            var model = _informationService.GetFooterContact();
            return PartialView("Footer/_FooterCopyrightPartial", model);
        }
        public ActionResult LoadProductHomePage(int Id)
        {
            var model = _productService.GetProductHomePageList(Id);
            if (Id == 6)
            {
                model.LinkYoutube = _productService.GetYoutubeLink();
            }
            return PartialView("_ProductHomePagePartial", model);
        }
        public ActionResult MiniCart()
        {
            var cart = ShoppingCart.Cart;
            return PartialView("Header/_MiniCartPartial", cart.Items);
        }
    }
}