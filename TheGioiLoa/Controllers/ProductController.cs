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
        public ActionResult Index(int? productId)
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
                Cover = product.Cover,
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

            return View(model);
        }
    }
}