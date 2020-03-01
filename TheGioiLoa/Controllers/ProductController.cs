using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Controllers
{
    public class ProductController : Controller
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();

        // GET: Product
        public ActionResult Details(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(productId);


            if (product == null || product.Status == 4 || product.Status == 2)
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
                    var parentCategory = db.Category.Find(item.ProductId);
                    if (parentCategory.CategoryParentId == null)
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

        public ActionResult Category()
        {
            ViewBag.BrandId = db.Brand.ToList();
            var model = db.Category.ToList();
            return View(model);
        }


        public ActionResult LoadProductCategory(int? categoryId, string sortBy)
        {
            var productList = new List<Product>();
            if (categoryId == null)
            {
                productList = db.Product.Where(a => a.Status == 1 || a.Status == 3).ToList();

            }
            else
            {
                var productIdList = db.CategoryProduct.Where(a => a.CategoryId == categoryId);
                foreach (var item in productIdList)
                {
                    var addProduct = db.Product.Find(item.ProductId);
                    if (addProduct.Status == 1 || addProduct.Status == 3)
                        productList.Add(addProduct);
                }
            }
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
    }
}