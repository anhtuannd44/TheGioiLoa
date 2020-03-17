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
        private readonly ImageService _imageService = new ImageService();
        private readonly BlogService _blogService = new BlogService();

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
        public ActionResult CreateCategory(CreateCategoryViewModel category)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _productService.CreateCategory(category);
                result.status = "success";
                result.message = "Thành công! Chuyên mục đã được tạo";
            }
            catch
            {
                result.status = "success";
                result.message = "Thành công! Chuyên mục đã được tạo";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryViewModel category)
        {
            var result = new JsonStatusViewModel();
            category.Name = _helper.DeleteSpace(category.Name);
            if (string.IsNullOrEmpty(category.Name))
            {
                try
                {
                    _productService.EditCategory(category);
                    result.status = "success";
                    result.message = "Thành công! Chuyên mục đã được chỉnh sửa.";
                }
                catch
                {
                    result.status = "error";
                    result.message = "Thất bại! Có lỗi xảy ra, vui long thử lại.";
                }
                return Json(result, JsonRequestBehavior.DenyGet);
            }
            result.status = "empty";
            result.message = "Thất bại! Tên chuyên mục không được để trống.";
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public PartialViewResult LoadEditCategoryPartial(int categoryId)
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
        public ActionResult RemoveCategory(int CategoryId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _productService.RemoveCategory(CategoryId);
                result.status = "success";
                result.message = "Thành công! Chuyên mục đã được xóa.";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ProductList()
        {
            var model = _productService.GetProductList();
            return View(model.ToList());
        }

        public ActionResult EditProduct(int productId, string error)
        {
            ViewBag.NotiPrice = error;
            var product = db.Product.Find(productId);
            if (product == null)
                return HttpNotFound();
            var model = _productService.GetAllInformationOfProductItem(product);
            if (!model.IsGetDataSuccess)
                return RedirectToAction("ProductList");
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(ProductViewModel product)
        {
            var error = "";
            if (product.Price == null && product.PriceSale != null || product.Price < product.PriceSale)
            {
                error = "Lỗi: Giá khuyến mãi phải nhỏ hơn giá bán!";
            }
            else
            {
                try
                {
                    //Edit ProductDb 
                    product.Name = _helper.DeleteSpace(product.Name);

                    _productService.EditProductDb(product);


                    //Remove and Add Category
                    _productService.RemoveCategoryProduct(product.ProductId);
                    _productService.AddCategoryToProduct(product.ProductId, Request["CategoryId"]);

                    //Remove andd Add Image
                    _productService.RemoveProductImage(product.ProductId);
                    _productService.AddImageToProduct(product.ProductId, product.Image);

                    //Remove add Add Tags
                    _productService.RemoveProductTag(product.ProductId);
                    _productService.AddTagToProduct(product.ProductId, product.Tag);
                    return RedirectToAction("ProductList");
                }
                catch {}
            }
            return RedirectToAction("EditProduct", new { productId = product.ProductId, error });

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
            if (product.Price == null && product.PriceSale != null || product.Price < product.PriceSale)
            {
                ViewBag.NotiPrice = "Lỗi: Giá khuyến mãi phải nhỏ hơn giá bán!";
            }
            else
            {
                try
                {
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
                }
            }
            ViewBag.BrandId = db.Brand.ToList();
            ViewBag.CategoryId = db.Category.ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                db.Product.Remove(db.Product.Find(productId));
                db.SaveChanges();
                result.status = "success";
                result.message = "Thành công! Sản phẩm đã được xóa";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
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

        [HttpPost]
        public ActionResult LoadLibraryImage(bool IsMultiple, string imageList, string selectedImage)
        {
            var image = db.Image.ToList();
            var model = new ListImageViewModel();
            var addImageList = new List<ImageViewModel>();
            foreach (var item in image)
            {
                var addItem = new ImageViewModel()
                {
                    ImageId = item.ImageId
                };
                addImageList.Add(addItem);
            }
            model.ImageList = addImageList.ToList();
            if (!string.IsNullOrEmpty(imageList))
            {
                var imageListArray = imageList.Split(',');
                foreach (var item in addImageList)
                {
                    foreach (var select in imageListArray)
                    {
                        if (select == item.ImageId)
                            item.IsSelected = true;
                    }
                }
            }
            else
            {
                foreach (var item in addImageList)
                {
                    if (selectedImage == item.ImageId)
                    {
                        item.IsSelected = true;
                        break;
                    }
                }
            }
            if (IsMultiple)
                model.IsMultiple = true;
            else
                model.IsMultiple = false;
            return PartialView("_ImageLibraryPartial", model);
        }

        [HttpPost]
        public ActionResult LoadSelectImage(string imageList)
        {
            var imageListArray = imageList.Split(',');
            List<Image> model = new List<Image>();
            foreach (var item in imageListArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var addItem = new Image()
                    {
                        ImageId = item
                    };
                    model.Add(addItem);
                }
            }
            return PartialView("_ImageSelectedPartial", model);
        }

        public ActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(CreateBrandViewModel brand)
        {
            var result = new JsonStatusViewModel();
            brand.Name = _helper.DeleteSpace(brand.Name);
            if (string.IsNullOrEmpty(brand.Name))
            {
                result.message = "Thất bại! Dữ liệu không được trống!";
                result.status = "empty";
            }
            else
            {
                try
                {
                    db.Brand.Add(new Brand()
                    {
                        Url = _helper.CreateUrl(brand.Name),
                        Name = brand.Name
                    });
                    db.SaveChanges();
                    result.message = "Thành công! Thương hiệu đã được chỉnh sửa!";
                    result.status = "success";
                }
                catch
                {
                    result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
                    result.status = "error";
                }
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public PartialViewResult LoadBrandList()
        {
            var model = new BrandViewModel();
            var brandList = db.Brand.ToList();
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

        [HttpPost]
        public ActionResult LoadEditBrandPartial(int brandId)
        {
            var model = db.Brand.Find(brandId);
            return PartialView("_EditBrandPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(Brand brand)
        {
            var result = new JsonStatusViewModel();
            try
            {
                var editItem = db.Brand.Find(brand.BrandId);
                editItem.Name = _helper.DeleteSpace(brand.Name);
                editItem.Url = _helper.CreateUrl(editItem.Name);
                db.Entry(editItem).State = EntityState.Modified;
                db.SaveChanges();

                result.message = "Thành công! Thương hiệu đã được chỉnh sửa!";
                result.status = "success";
            }
            catch
            {
                result.message = "Có lỗi xảy ra, vui lòng thử lại!";
                result.status = "error";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBrand(int brandId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                var removeBrand = db.Brand.Find(brandId);
                db.Brand.Remove(removeBrand);
                db.SaveChanges();
                result.message = "Thành công! Xóa thành công thương hiệu!";
                result.status = "success";
            }
            catch
            {
                result.message = "Có lỗi xảy ra, vui lòng thử lại!";
                result.status = "error";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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
            return Json(json, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult LoadBlogList()
        {
            var model = _blogService.GetBlogList("All", 1).ToList();
            return PartialView("_BlogListPartial", model);
        }
        public ActionResult CreateBlog()
        {
            var categories = db.BlogCategory.ToList();
            var model = new BlogViewModel
            {
                CategoryList = categories,
                StatusList = _blogService.GetStatus()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlog(BlogViewModel blog)
        {
            try
            {
                _blogService.CreateBlog(blog, 1);
                return RedirectToAction("Blog");
            }
            catch
            {
                var categories = db.BlogCategory.ToList();
                blog.CategoryList = categories;
                blog.StatusList = _blogService.GetStatus();
                return View(blog);
            }
        }
        public ActionResult EditBlog(int blogId)
        {
            var blog = db.Blog.Find(blogId);
            if (blog == null)
                return HttpNotFound();
            var model = new BlogViewModel()
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                BlogContent = blog.BlogContent,
                Status = blog.Status,
                BlogCategoryId = blog.BlogCategoryId
            };
            var categories = db.BlogCategory.ToList();
            model.CategoryList = categories;
            model.StatusList = _blogService.GetStatus();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBlog(BlogViewModel blog)
        {
            try
            {
                _blogService.EditBlog(blog);
                return RedirectToAction("Blog");
            }
            catch
            {
                var categories = db.BlogCategory.ToList();
                blog.CategoryList = categories;
                blog.StatusList = _blogService.GetStatus();
                return View(blog);
            }
        }

        [HttpPost]
        public ActionResult DeleteBlog(int blogId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                db.Blog.Remove(db.Blog.Find(blogId));
                db.SaveChanges();
                result.status = "success";
                result.message = "Thành công! Bài viết đã được xóa.";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại.";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult LoadBlogCategory()
        {
            return PartialView("_BlogCategory", db.BlogCategory.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlogCategory(string blogCategoryName)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _blogService.CreateBlogCategory(blogCategoryName);
                result.status = "success";
                result.message = "Thành công " + blogCategoryName + " đã được tạo!";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại!";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult EditBlogCategory(int blogCategoryId, string Name)
        {
            var result = new JsonStatusViewModel();
            if (string.IsNullOrEmpty(Name))
            {
                result.status = "empty";
                result.message = "Thất bại! Tên chuyên mục vui lòng không để trống!";
            }
            else
            {
                try
                {
                    _blogService.EditBlogCateogry(blogCategoryId, Name);
                    result.status = "success";
                    result.message = "Thành công! Danh mục đã được chỉnh sửa!";
                }
                catch
                {
                    result.status = "error";
                    result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
                }
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteBlogCategory(int blogCategoryId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _blogService.DeleteBlogCategory(blogCategoryId);
                result.status = "success";
                result.message = "Thành công! Danh mục đã được xóa!";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại!";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadImageSummerNote(HttpPostedFileBase aUploadedFile)
        {
            var vReturnImagePath = string.Empty;
            if (aUploadedFile.ContentLength > 0)
            {
                string imageName = _helper.RandomString() + Path.GetExtension(aUploadedFile.FileName);
                _imageService.SaveImageInDb(imageName);
                aUploadedFile.SaveAs(Server.MapPath("/Content/Upload/Images/") + imageName);
                vReturnImagePath = "/Content/Upload/Images/" + imageName;
            }
            return Json(Convert.ToString(vReturnImagePath), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteImageSummerNote(string imageName)
        {
            var result = new JsonStatusViewModel();
            try
            {
                var deleteImageInDbStatus = _imageService.RemoveImageInDb(imageName);
                if (deleteImageInDbStatus == 1)
                {
                    string fullPath = Request.MapPath("/Content/Upload/Images/" + imageName);
                    _imageService.RemoveImageInServer(fullPath);
                }
                result.status = "success";
                result.message = "Xóa hình ảnh thành công";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại!";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Page()
        {
            var model = _blogService.GetBlogList("All", 2).ToList();
            return View(model);
        }
        public ActionResult LoadPageList()
        {
            var model = _blogService.GetBlogList("All", 2).ToList();
            return PartialView("_PageListPartial", model);
        }
        public ActionResult CreatePage()
        {
            var model = new BlogViewModel
            {
                StatusList = _blogService.GetStatus()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePage(BlogViewModel page)
        {
            try
            {
                _blogService.CreateBlog(page, 2);
                return RedirectToAction("Page");
            }
            catch
            {
                page.StatusList = _blogService.GetStatus();
                return View(page);
            }
        }
        public ActionResult EditPage(int pageId)
        {
            var page = db.Blog.Find(pageId);
            if (page == null)
                return HttpNotFound();
            var model = new BlogViewModel()
            {
                BlogId = page.BlogId,
                Title = page.Title,
                BlogContent = page.BlogContent,
                Status = page.Status
            };
            model.StatusList = _blogService.GetStatus();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPage(BlogViewModel page)
        {
            try
            {
                _blogService.EditBlog(page);
                return RedirectToAction("Page");
            }
            catch
            {
                page.StatusList = _blogService.GetStatus();
                return View(page);
            }
        }

        [HttpPost]
        public ActionResult DeletePage(int pageId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                db.Blog.Remove(db.Blog.Find(pageId));
                db.SaveChanges();
                result.status = "success";
                result.message = "Thành công! Trang đã được xóa.";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại.";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}
