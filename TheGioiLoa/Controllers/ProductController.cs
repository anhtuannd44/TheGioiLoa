using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class ProductController : Controller
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();
        private readonly ProductService _productService = new ProductService();
        private readonly ApplicationDbContext dbApp = new ApplicationDbContext();
        // GET: Product
        public ActionResult Details(int? productId, string url)
        {
            if (productId == null || string.IsNullOrEmpty(url))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Product.Find(productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            if (product.Url.ToLower() != _helper.DeleteSpace(url.ToLower()) || product.Status == 4 || product.Status == 2)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult AllCategory()
        {
            var model = db.Category.ToList();
            return View(model);
        }

        public ActionResult Category(int? categoryId, string url)
        {
            var model = new List<Category>();
            if (categoryId == null && !string.IsNullOrEmpty(url) || (categoryId != null && string.IsNullOrEmpty(url)))
            {
                return HttpNotFound();
            }
            if (categoryId == null && string.IsNullOrEmpty(url))
            {
                ViewBag.BrandId = db.Brand.ToList();
                model = db.Category.ToList();
                ViewBag.Title = "Cửa Hàng";
                ViewBag.CategoryName = "Tất cả sản phẩm";
            }
            else
            {
                var category = db.Category.Find(categoryId);

                if (category == null)
                {
                    return HttpNotFound();
                }
                if (category.Url != url)
                {
                    return HttpNotFound();
                }
                if (category.CategoryParentId == null)
                {
                    var childCategory = db.Category.Where(a => a.CategoryParentId == category.CategoryId).ToList();
                    if (childCategory.Count() == 0)
                        model = db.Category.Where(a => a.CategoryParentId == null).ToList();
                    else
                        model = childCategory;
                }
                else
                {
                    var childCategory = db.Category.Where(a => a.CategoryParentId == category.CategoryId).ToList();
                    if (childCategory.Count() == 0)
                        model = db.Category.Where(a => a.CategoryParentId == category.CategoryParentId).ToList();
                    else
                        model = childCategory;
                }
                ViewBag.BrandId = db.Brand.ToList();
                ViewBag.Title = db.Category.Find(categoryId).Name;

            }
            ViewBag.CategoryId = categoryId;
            return View(model);
        }

        public ActionResult LoadProductCategory(int? categoryId, string sortBy, int priceSortFrom, int priceSortTo)
        {
            var model = _productService.GetProductLists(categoryId, sortBy, priceSortFrom, priceSortTo, null);
            return PartialView("_ProductInCategory", model);
        }

        public ActionResult ProductByCategory(int? categoryId, string Url)
        {
            return View();
        }

        public ActionResult AddReview(int productId)
        {
            var model = new Review()
            { 
                ProductId = productId
            };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = dbApp.Users.Find(userId);
                model.UserName = user.FullName;
                model.Phone = user.PhoneNumber;
                model.Email = user.Email;
            }
            return PartialView("_ReviewFormPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(Review review)
        {
            try
            {
                db.Review.Add(review);
                db.SaveChanges();
                var json = new
                {
                    status = "success",
                    message = "Cảm ơn! Đánh giá của bạn đã được chúng tôi ghi nhận"
                };
                return Json(json, JsonRequestBehavior.DenyGet);

            }
            catch
            {
                var json = new
                {
                    status = "error",
                    message = "Có lỗi xảy ra, vui lòng thử lại"
                };
                return Json(json, JsonRequestBehavior.DenyGet);
            }
        }


        public ActionResult LoadReview(int productId)
        {
            var model = new ReviewViewModel
            {
                AvgStar = 0
            };
            var review = db.Review.Where(a => a.ProductId == productId);
            model.CommentCount = review.Count();
            var sumStar = 0;
            var listReview = new List<EachReviewViewModel>();
            for (int i = 1; i <= 5; i++)
            {
                var addReview = new EachReviewViewModel()
                {
                    Star = i,
                    Count = review.Where(a => a.StarCount == i).Count(),
                    Percent = Math.Round((double)review.Where(a => a.StarCount == i).Count() / (double)model.CommentCount * 100)
                };
                sumStar += review.Where(a => a.StarCount == i).Count() * i;
                listReview.Add(addReview);
            }
            model.AvgStar = Math.Round((double)sumStar / (double)review.Count());
            model.EachReviewViewModel = listReview;
            return PartialView("_ReviewPartial", model);
        }

        public ActionResult LoadStarReview(int productId)
        {
            var model = new ReviewViewModel
            {
                AvgStar = 0
            };
            var review = db.Review.Where(a => a.ProductId == productId);
            model.CommentCount = review.Count();
            var sumStar = 0;
            var listReview = new List<EachReviewViewModel>();
            for (int i = 1; i <= 5; i++)
            {
                var addReview = new EachReviewViewModel()
                {
                    Star = i,
                    Count = review.Where(a => a.StarCount == i).Count(),
                    Percent = Math.Round((double)review.Where(a => a.StarCount == i).Count() / (double)model.CommentCount * 100)
                };
                sumStar += review.Where(a => a.StarCount == i).Count() * i;
                listReview.Add(addReview);
            }
            model.AvgStar = Math.Round((double)sumStar / (double)review.Count());
            model.EachReviewViewModel = listReview;
            return PartialView("_StarReviewPartial", model);
        }
        public ActionResult NewestProductSidebar()
        {
            var model = _productService.GetProductLists(null, "newest", null, null, 4);
            return PartialView("_NewestProductSidebarPartial", model);
        }

        public ActionResult GetHotline()
        {
            ViewBag.Hotline = db.Information.Find("Main").Hotline;
            return PartialView("_HotlinePartial");
        }
    }
}