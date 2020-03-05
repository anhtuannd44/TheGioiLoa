﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Controllers
{
    public class ProductController : Controller
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private HelperFunction _hepler = new HelperFunction();
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

            if (product.Url.ToLower() != _hepler.DeleteSpace(url.ToLower()) || product.Status == 4 || product.Status == 2)
            {
                return HttpNotFound();
            }

            var model = new ProductViewModel
            {
                Name = product.Name,
                Brands = db.Brand.Find(product.BrandId),
                ProductId = product.ProductId,
                Description = product.Description,
                Details = product.Details,
                CoverName = product.Cover,
                Characteristics = product.Characteristics,
                Promotion = product.Promotion,
                Videos = product.Videos,
                Price = product.Price,
                ListedPrice = product.ListedPrice,
                Status = product.Status
            };
            var image = db.Product_Image.Include(a => a.Image).Where(a => a.ProductId == productId);
            var addImage = new List<Image>();
            foreach (var item in image)
            {
                addImage.Add(new Image()
                {
                    ImageId = item.ImageId
                });
            }
            model.Images = addImage;

            var tag = db.Product_Tag.Include(a => a.Tag).Where(a => a.ProductId == productId);
            var addTag = new List<Tag>();
            foreach (var item in tag)
            {
                addTag.Add(new Tag()
                {
                    TagId = item.TagId,
                    Name = item.Tag.Name
                });
            }
            model.Tags = addTag;

            var category = db.CategoryProduct.Where(a => a.ProductId == productId);
            if (category.Count() != 0)
            {
                foreach (var item in category)
                {
                    var parentCategoryId = db.Category.Find(item.ProductId);
                    if (parentCategoryId != null)
                        if (parentCategoryId.CategoryParentId == null)
                        {
                            var categoryProductRelateds = db.CategoryProduct.Where(a => a.ProductId == productId && a.CategoryId == item.CategoryId).Take(4);
                            var productRelateds = new List<Product>();
                            foreach (var addItem in categoryProductRelateds)
                            {
                                productRelateds.Add(db.Product.Find(addItem.ProductId));
                            }
                            model.ProductRelateds = productRelateds;
                            break;
                        }
                }
            }

            return View(model);

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
            var productLists = new List<Product>();
            if (categoryId == null)
            {
                productLists = db.Product.Where(a => a.Status == 1 || a.Status == 3)/*.Where(a => a.Price >= priceSortFrom && a.Price <= priceSortTo)*/.ToList();

            }
            else
            {
                var productIdList = db.CategoryProduct.Where(a => a.CategoryId == categoryId).ToList();
                foreach (var item in productIdList)
                {
                    var addProduct = db.Product.Find(item.ProductId);
                    if ((addProduct.Status == 1 || addProduct.Status == 3)/* && addProduct.Price >= priceSortFrom && addProduct.Price <= priceSortTo*/)
                        productLists.Add(addProduct);
                }
            }
            var productList = new List<Product>();

            if (priceSortFrom == 0)
                productList = productLists.Where(a => a.Price <= priceSortTo || a.Price == null).ToList();

            switch (sortBy)
            {
                case "newest":
                    productList = productList.OrderBy(a => a.DateCreated).ToList();
                    break;
                case "priceLowToHigh":
                    productList = productList.OrderBy(a => a.Price).ToList();
                    break;
                case "priceHighToLow":
                    productList = productList.OrderByDescending(a => a.Price).ToList();
                    break;
                case "nameAsc":
                    productList = productList.OrderBy(a => a.Price).ToList();
                    break;
            }

            return PartialView("_ProductInCategory", productList);

        }

        public ActionResult ProductByCategory(int? categoryId, string Url)
        {

            return View();
        }
    }
}