using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;
using Unity;

namespace TheGioiLoa.Controllers
{
    public class AdminController : Controller
    {

        //private readonly ICategoryService _categoryService;
        private readonly ProductService _productService = new ProductService();

        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();

        // GET: Admin
        public ActionResult Categories()
        {
            var parentList = db.Category.ToList();
            return View(parentList);
        }

        public ActionResult LoadCategoryList()
        {
            var category = db.Category;
            var model = new CategoryViewModel()
            {
                CategoryList = category.ToList()
            };
            if (category != null)
                model.Notification = "nodata";

            ViewBag.CategoryParentList = db.Category.Where(a => a.CategoryParentId == null).ToList();
            return PartialView("_CategoryListPartial", model);
        }

        [HttpPost]
        public PartialViewResult LoadCategorySelect()
        {
            var model = db.Category.ToList();
            return PartialView("_SelectListCategory", model);
        }
        [HttpPost]
        public string CreateCategory(CreateCategoryViewModel category)
        {
            try
            {
                category.Name = _helper.DeleteSpace(category.Name);
                db.Category.Add(new Category()
                {
                    Name = category.Name,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Url = _helper.CreateUrl(category.Name),
                    CategoryParentId = category.CategoryParentId
                });
                db.SaveChanges();
                return "successed";
            }
            catch
            {
                return "error";
            }
        }

        [HttpPost]
        public string EditCategory(EditCategoryViewModel category)
        {
            category.Name = _helper.DeleteSpace(category.Name);
            if (string.IsNullOrEmpty(category.Name))
                return "empty";
            else
            {
                try
                {
                    var editItem = db.Category.Find(category.CategoryId);

                    editItem.DateModified = DateTime.Now;
                    editItem.Name = category.Name;
                    editItem.Url = _helper.CreateUrl(category.Name);

                    db.Entry(editItem).State = EntityState.Modified;
                    db.SaveChanges();
                    return "successed";
                }
                catch
                {
                    return "editfaild";
                }
            }
        }

        [HttpPost]
        public PartialViewResult LoadEditPartial(int categoryId)
        {
            var editItem = db.Category.Find(categoryId);
            var model = new EditCategoryViewModel()
            {
                Name = editItem.Name,
                CategoryId = editItem.CategoryId
            };
            return PartialView("_EditCategoryPartial", model);
        }

        [HttpPost]
        public string RemoveCategory(RemoveCategoryViewMode category)
        {
            try
            {
                var removeItem = db.Category.Find(category.CategoryId);
                db.Category.Remove(removeItem);
                db.CategoryProduct.RemoveRange(db.CategoryProduct.Where(a => a.CategoryId == category.CategoryId));

                var childCategoryList = db.Category.Where(a => a.CategoryParentId == category.CategoryId);
                if (childCategoryList != null)
                    foreach (var child in childCategoryList)
                    {
                        db.Category.RemoveRange(db.Category.Where(a => a.CategoryParentId == child.CategoryId || a.CategoryId == child.CategoryId));
                        db.CategoryProduct.RemoveRange(db.CategoryProduct.Where(a => a.CategoryId == child.CategoryId));
                        var subChildList = db.Category.Where(a => a.CategoryParentId == child.CategoryId);
                        if (subChildList != null)
                            foreach (var subChild in subChildList.ToList())
                            {
                                db.CategoryProduct.RemoveRange(db.CategoryProduct.Where(a => a.CategoryId == subChild.CategoryId));
                            }
                    }

                db.SaveChanges();
                return "successed";
            }
            catch
            {
                return "error";
            }
        }

        public ActionResult ProductList()
        {
            var model = new List<ProductViewModel>();

            var productList = db.Product.Where(a => a.Status != 4);
            foreach (var item in productList)
            {
                var addProduct = new ProductViewModel()
                {
                    ProductId = item.ProductId,
                    Description = item.Description,
                    CoverName = item.Cover,
                    ListedPrice = item.ListedPrice,
                    Price = item.Price,
                    Status = item.Status,
                    Name = item.Name,
                    Url = item.Url
                };
                var Categories = db.CategoryProduct.Include(a => a.Category).Where(a => a.ProductId == item.ProductId);
                if (Categories.Count() != 0)
                {
                    var addListCategory = new List<CategoryProductEditViewModel>();
                    foreach (var category in Categories)
                    {
                        addListCategory.Add(new CategoryProductEditViewModel()
                        {
                            CategoryId = category.CategoryId,
                            CategoryParentId = category.Category.CategoryParentId,
                            Name = category.Category.Name
                        });
                    }
                    addProduct.Categories = addListCategory.ToList();
                }
                var Images = db.Product_Image.Where(a => a.ProductId == item.ProductId);
                if (Images.Count() != 0)
                {
                    var addListImage = new List<Image>();
                    foreach (var image in Images)
                    {
                        addListImage.Add(new Image()
                        {
                            ImageId = image.ImageId
                        });
                    }
                    addProduct.Images = addListImage;
                }
                model.Add(addProduct);
            }

            return View(model.ToList());
        }


        public ActionResult EditProduct(int productId)
        {
            var product = db.Product.Find(productId);
            if (product == null)
                return HttpNotFound();

            var model = new ProductViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Url = _helper.CreateUrl(product.Name),
                Description = product.Description,
                DateModified = DateTime.Now,
                Price = product.Price,
                ListedPrice = product.ListedPrice,
                Status = product.Status,
                Characteristics = product.Characteristics,
                Promotion = product.Promotion,
                Videos = product.Videos,
                Details = product.Details,
                CoverName = product.Cover
            };
            var category = db.Category.ToList();
            var addCategoryList = new List<CategoryProductEditViewModel>();
            foreach (var item in category)
            {
                addCategoryList.Add(new CategoryProductEditViewModel()
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    CategoryParentId = item.CategoryParentId,
                    IsChecked = (db.CategoryProduct.Find(item.CategoryId, productId) != null) ? true : false
                });
            }
            model.Categories = addCategoryList.ToList();

            var image = db.Product_Image.Include(a => a.Image).Where(a => a.ProductId == productId).ToList();
            var addImageList = new List<Image>();
            foreach (var item in image)
            {
                addImageList.Add(new Image()
                {
                    ImageId = item.ImageId
                });
            }
            model.Images = addImageList.ToList();

            var brand = db.Brand.ToList();
            var addBrandList = new List<BrandSelectedViewModel>();
            foreach (var item in brand)
            {
                addBrandList.Add(new BrandSelectedViewModel()
                {
                    BrandId = item.BrandId,
                    Name = item.Name,
                    IsChecked = (db.Product.Find(productId).BrandId == item.BrandId) ? true : false
                });
            }
            model.Brand = addBrandList.ToList();

            var tag = db.Product_Tag.Include(a => a.Tag).Where(a => a.ProductId == productId);
            var addTagList = new List<Tag>();
            foreach (var item in tag)
            {
                addTagList.Add(new Tag()
                {
                    TagId = item.TagId,
                    Name = item.Tag.Name
                });
            }
            model.Tags = addTagList.ToList();

            model.StatusList = _productService.GetStatus();


            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(ProductViewModel product)
        {
            if (product.Cover != null)
            {
                var fileName = _helper.RandomString() + Path.GetExtension(product.Cover.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Upload/Images/"), fileName);
                product.Cover.SaveAs(path);
                product.CoverName = fileName;
            }
            else
            {
                product.CoverName = "No_Picture.jpg";
            }

            //Edit ProductDb 
            product.Name = _helper.DeleteSpace(product.Name);
            //try
            //{
            _productService.EditProductDb(product);
            _productService.RemoveCategoryProduct(product.ProductId);
            //Add Category
            _productService.AddCategoryToProduct(product.ProductId, Request["CategoryId"]);

            //Add Image
            _productService.RemoveProductImage(product.ProductId);
            _productService.AddImageToProduct(product.ProductId, product.Image);

            //Add Tags
            _productService.RemoveProductTag(product.ProductId);
            _productService.AddTagToProduct(product.ProductId, product.Tag);

            return RedirectToAction("ProductList");
            //}
            //catch
            //{
            //    ViewBag.BrandId = db.Brand.ToList();
            //    ViewBag.CategoryId = db.Category.ToList();
            //    return View(product);
            //}

        }
        public ActionResult CreateProduct()
        {
            ViewBag.BrandId = db.Brand.ToList();
            ViewBag.CategoryId = db.Category.ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateProduct(CreateProductViewModel product)
        {
            product.Name = _helper.DeleteSpace(product.Name);
            try
            {
                if (product.Cover != null)
                {
                    var fileName = _helper.RandomString() + Path.GetExtension(product.Cover.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Upload/Images/"), fileName);
                    product.Cover.SaveAs(path);
                    product.CoverName = fileName;
                }
                else
                {
                    product.CoverName = "No_Picture.jpg";
                }
                _productService.AddProductToDb(product);

                var productId = _productService.GetLastestProductId(product.Name);

                //Add Category
                _productService.AddCategoryToProduct(productId, Request["CategoryId"]);

                //Add Image
                _productService.AddImageToProduct(productId, product.Image);

                //Add Tags
                _productService.AddTagToProduct(productId, product.Tag);





                return RedirectToAction("ProductList");
            }
            catch
            {
                ViewBag.BrandId = db.Brand.ToList();
                ViewBag.CategoryId = db.Category.ToList();
                return View(product);
            }

        }

        public JsonResult UploadImage(HttpPostedFileBase File)
        {
            UploadImageViewModel result = new UploadImageViewModel();
            try
            {
                var fileName = _helper.RandomString() + Path.GetExtension(File.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Upload/Images/"), fileName);
                File.SaveAs(path);
                result.status = "ok";
                result.path = path;
                db.Image.Add(new Image() { ImageId = fileName, DateCreated = DateTime.Now });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                result.status = "error";
                result.message = ex.Message;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadLibraryImage()
        {
            var model = db.Image.ToList();
            return PartialView("_ImageLibraryPartial", model);
        }

        public ActionResult LoadSelectImage(string imageList)
        {
            var imageListArray = imageList.Split('|');
            List<Image> model = new List<Image>();
            foreach (var item in imageListArray)
            {
                var addItem = new Image()
                {
                    ImageId = item
                };
                model.Add(addItem);
            }
            return PartialView("_ImageSelectedPartial", model);
        }

        public ActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string CreateBrand(CreateBrandViewModel brand)
        {
            if (!string.IsNullOrEmpty(brand.Name))
            {
                try
                {
                    brand.Name = _helper.DeleteSpace(brand.Name);
                    db.Brand.Add(new Brand()
                    {
                        Url = _helper.CreateUrl(brand.Name),
                        Name = brand.Name
                    });
                    db.SaveChanges();
                    return "successed";
                }
                catch
                {
                    return "error";
                }
            }
            else return "empty";
        }

        [HttpPost]
        public PartialViewResult LoadBrandList()
        {
            var model = new BrandViewModel();
            var brandList = db.Brand;
            if (brandList.Count() != 0)
            {
                model.BrandList = brandList.ToList();
                return PartialView("_BrandListPartial", model);
            }
            else
            {
                return PartialView("Admin/_NullDataPartial");
            }
        }

        public JsonResult CreateTag(string tag)
        {
            var addTag = db.Tag.Where(a => a.Name == tag).FirstOrDefault();
            if (addTag == null)
            {
                db.Tag.Add(new Tag()
                {
                    Name = tag
                });
                db.SaveChanges();
            }
            var result = db.Tag.Where(a => a.Name == tag).FirstOrDefault();
            TagViewModel json = new TagViewModel() { TagId = result.TagId.ToString(), Name = result.Name };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
