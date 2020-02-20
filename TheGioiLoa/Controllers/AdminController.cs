using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;

namespace TheGioiLoa.Controllers
{
    public class AdminController : Controller
    {

        public ICategoryService CategoryService { get; set; }

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
                    Url = _helper.CreateUrl(category.Name)
                });
                var parent = db.Category.Find(category.CategoryParentId);
                if (parent == null)
                    return "error";
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
            return View();
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
            //try
            {
                product.Name = _helper.DeleteSpace(product.Name);
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
                    Videos = product.Video,
                    Details = product.Details
                };
                db.Product.Add(addProduct);
                db.SaveChanges();


                var productId = db.Product.Where(a => a.Name == product.Name).OrderByDescending(x => x.ProductId).Take(1).Single().ProductId;

                //Add Category
                var categoryIdList = Request["CategoryId"];
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

                //Add Image
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var imageListArray = product.Image.Split('|');
                    foreach (var item in imageListArray)
                    {
                        db.Product_Image.Add(new Product_Image()
                        {
                            ImageId = item,
                            ProductId = productId,
                            IsMain = false
                        });
                        db.SaveChanges();
                    }
                }

                //Add Tags
                if (!string.IsNullOrEmpty(product.Tag))
                {
                    var imageListArray = product.Tag.Split(',');
                    foreach (var item in imageListArray)
                    {
                        db.Tag.Add(new Tag()
                        {
                            Name = item,
                        });
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("ProductList");
            }
            //catch
            //{
            //    ViewBag.BrandId = db.Brand.ToList();
            //    ViewBag.CategoryId = db.Category.ToList();
            //    return View(product);
            //}

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
            string[] filePaths = Directory.GetFiles(Server.MapPath("/Content/Upload/Images/"));
            List<GetFileViewModel> model = new List<GetFileViewModel>();
            foreach (var item in filePaths)
            {
                var addItem = new GetFileViewModel()
                {
                    FileName = item.Substring(item.LastIndexOf("\\") + 1)
                };
                model.Add(addItem);
            }
            return PartialView("_ImageLibraryPartial", model);

        }

        public ActionResult LoadSelectImage(string imageList)
        {
            var imageListArray = imageList.Split('|');
            List<GetFileViewModel> model = new List<GetFileViewModel>();
            foreach (var item in imageListArray)
            {
                var addItem = new GetFileViewModel()
                {
                    FileName = item
                };
                model.Add(addItem);
            }
            return PartialView("_ImageSelectedPartial", model);
        }
    }
}
