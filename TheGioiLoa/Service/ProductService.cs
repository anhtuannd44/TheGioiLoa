using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Service
{

    public class ProductService
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private HelperFunction _helper = new HelperFunction();

        public int GetLastestProductId(string productName)
        {
            return db.Product.Where(a => a.Name == productName).OrderByDescending(x => x.ProductId).Take(1).Single().ProductId;
        }

        public void AddProductToDb(CreateProductViewModel product)
        {
            var addProduct = new Product()
            {
                Name = product.Name,
                Url = _helper.CreateUrl(product.Name),
                BrandId = product.BrandId,
                Description = product.Description,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Price = product.Price,
                ListedPrice = product.ListedPrice,
                Status = product.Status,
                Characteristics = product.Characteristics,
                Promotion = product.Promotion,
                Videos = product.Videos,
                Details = product.Details
            };
            db.Product.Add(addProduct);
            db.SaveChanges();
        }

        public void AddCategoryToProduct(int productId, string categoryIdList)
        {
            if (!string.IsNullOrEmpty(categoryIdList))
            {

                string[] categoryIdArray = categoryIdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < categoryIdArray.Length; i++)
                {
                    db.CategoryProduct.Add(new CategoryProducts()
                    {
                        ProductId = productId,
                        CategoryId = Convert.ToInt32(categoryIdArray[i])
                    });
                }
                db.SaveChanges();
            }
        }

        public void AddImageToProduct(int productId, string imageList)
        {
            if (!string.IsNullOrEmpty(imageList))
            {
                var imageListArray = imageList.Split('|');
                foreach (var item in imageListArray)
                {
                    db.Product_Image.Add(new Product_Image()
                    {
                        ImageId = item,
                        ProductId = productId
                    });
                }
                db.SaveChanges();
            }
        }

        public void AddTagToProduct(int productId, string tagList)
        {
            if (!string.IsNullOrEmpty(tagList))
            {
                var imageListArray = tagList.Split(',');
                List<Tag> addTag = new List<Tag>();
                foreach (var item in imageListArray)
                {
                    var tagId = db.Tag.Where(a => a.Name == item).FirstOrDefault();
                    if (tagId != null)
                        db.Product_Tag.Add(new Product_Tag()
                        {
                            ProductId = productId,
                            TagId = tagId.TagId
                        });
                }
                db.SaveChanges();
            }
        }
    }
}