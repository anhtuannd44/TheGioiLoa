using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;
using TheGioiLoa.Service;
using PagedList;
using PagedList.Mvc;

namespace TheGioiLoa.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        //private readonly ICategoryService _categoryService;
        private readonly ProductService _productService = new ProductService();
        private readonly ImageService _imageService = new ImageService();
        private readonly BlogService _blogService = new BlogService();
        private readonly InformationService _informationService = new InformationService();

        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        //
        public ActionResult LoadCategoryList()
        {
            var model = db.Category.ToList();
            return PartialView("ProductAndCategory/_CategoryListPartial", model);
        }

        public PartialViewResult LoadCategorySelect()
        {
            var model = db.Category.ToList();
            return PartialView("ProductAndCategory/_SelectListCategory", model);
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
            if (!string.IsNullOrEmpty(category.Name))
            {
                try
                {
                    _productService.EditCategory(category);
                    result.status = "success";
                    result.message = "Thành công! Chuyên mục đã được chỉnh sửa";
                }
                catch
                {
                    result.status = "error";
                    result.message = "Thất bại! Có lỗi xảy ra, vui long thử lại";
                }
                return Json(result, JsonRequestBehavior.DenyGet);
            }
            result.status = "empty";
            result.message = "Thất bại! Tên chuyên mục không được để trống";
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
            return PartialView("ProductAndCategory/_EditCategoryPartial", model);
        }


        [HttpPost]
        public ActionResult RemoveCategory(int CategoryId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _productService.RemoveCategory(CategoryId);
                result.status = "success";
                result.message = "Thành công! Chuyên mục đã được xóa";
            }
            catch (Exception ex)
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ProductList(int? page)
        {
            var model = db.Product.OrderByDescending(a => a.DateCreated).ToPagedList(page ?? 1, 10);
            return View(model);
        }

        public ActionResult EditProduct(int productId)
        {
            var product = db.Product.Find(productId);
            if (product == null)
                return HttpNotFound();
            ViewBag.Brand = db.Brand.ToList();
            ViewBag.Categories = _productService.GetCategoryOfProduct(productId);
            ViewBag.StatusList = _productService.GetStatus();
            return View("ProductAndCategory/EditProduct", product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(Product product, string imageAlbum)
        {
            if (!string.IsNullOrEmpty(product.Name))
            {
                product.Name = _helper.DeleteSpace(product.Name);
                product.Url = _helper.CreateUrl(product.Name);
                product.DateModified = DateTime.Now;

                if (product.Price == null && product.PriceSale != null || product.Price < product.PriceSale)
                {
                    ViewBag.NotiPrice = "Lỗi: Giá khuyến mãi phải nhỏ hơn giá bán";
                }
                else
                {
                    try
                    {
                        _productService.EditProduct(product);
                        //Remove and Add Category
                        _productService.RemoveCategoryProduct(product.ProductId);
                        _productService.AddCategoryToProduct(product.ProductId, Request["CategoryId"]);

                        //Remove andd Add Image
                        _productService.RemoveProductImage(product.ProductId);
                        _productService.AddImageToProduct(product.ProductId, imageAlbum);

                        return RedirectToAction("ProductList");
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            ViewBag.Brand = db.Brand.ToList();
            ViewBag.Categories = _productService.GetCategoryOfProduct(product.ProductId);
            ViewBag.StatusList = _productService.GetStatus();
            return View("ProductAndCategory/EditProduct", product);

        }
        public ActionResult CreateProduct()
        {
            ViewBag.BrandId = db.Brand.ToList();
            ViewBag.CategoryId = db.Category.ToList();
            return View("ProductAndCategory/CreateProduct");
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product, string imageAlbum)
        {
            if (!string.IsNullOrEmpty(product.Name))
            {
                product.Name = _helper.DeleteSpace(product.Name);
                product.Url = _helper.CreateUrl(product.Name);
                product.DateCreated = DateTime.Now;

                if (product.Price == null && product.PriceSale != null || product.Price < product.PriceSale)
                {
                    ViewBag.NotiPrice = "Lỗi: Giá khuyến mãi phải nhỏ hơn giá bán";
                }
                else
                {
                    try
                    {
                        db.Product.Add(product);
                        db.SaveChanges();
                        var productId = _productService.GetLastestProductId(product.Name);
                        //Add Category
                        _productService.AddCategoryToProduct(productId, Request["CategoryId"]);
                        //Add ImageList
                        _productService.AddImageToProduct(productId, imageAlbum);
                        return RedirectToAction("ProductList");
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            ViewBag.BrandId = db.Brand.ToList();
            ViewBag.CategoryId = db.Category.ToList();
            return View("ProductAndCategory/CreateProduct", product);
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
                result.message = "Thành công! Đã xóa sản phẩm";
            }
            catch (Exception ex)
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
        public ActionResult DeleteImage(string imageId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                var deleteImageInDbStatus = _imageService.RemoveImageInDb(imageId);
                if (deleteImageInDbStatus == 1)
                {
                    string fullPath = Request.MapPath("/Content/Upload/Images/" + imageId);
                    _imageService.RemoveImageInServer(fullPath);
                }
                result.status = "success";
                result.message = "Xóa hình ảnh thành công";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult LoadLibraryImage(string target, bool IsMultiple, string selectedImage, string notLoadAll)
        {
            var image = db.Image.OrderByDescending(a => a.DateCreated).ToList();
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
            if (!string.IsNullOrEmpty(selectedImage))
            {
                var imageListArray = selectedImage.Split(',');
                foreach (var item in addImageList)
                {
                    foreach (var select in imageListArray)
                    {
                        if (select == item.ImageId)
                            item.IsSelected = true;
                    }
                }
            }

            if (IsMultiple)
                model.IsMultiple = true;
            else
                model.IsMultiple = false;
            model.Target = target;


            if (notLoadAll == "notLoadAll")
                return PartialView("_ImageLibraryPartial", model);

            return PartialView("_ImageLibraryModalPartial", model);
        }


        public ActionResult LoadSelectImage(string imageAlbum)
        {
            var imageListArray = imageAlbum.Split(',');
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
                result.message = "Thất bại! Dữ liệu không được trống";
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
                    result.message = "Thành công! Thương hiệu đã được chỉnh sửa";
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
                return PartialView("Brand/_BrandListPartial", model);
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
            return PartialView("Brand/_EditBrandPartial", model);
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

                result.message = "Thành công! Thương hiệu đã được chỉnh sửa";
                result.status = "success";
            }
            catch
            {
                result.message = "Có lỗi xảy ra, vui lòng thử lại";
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
                result.message = "Thành công! Xóa thành công thương hiệu";
                result.status = "success";
            }
            catch
            {
                result.message = "Có lỗi xảy ra, vui lòng thử lại";
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

        public ActionResult Blog(int? page)
        {
            var model = _blogService.GetBlogList("All", 1).OrderByDescending(a => a.DateCreated).ToPagedList(page ?? 1, 10);
            return View(model);
        }
        public ActionResult CreateBlog()
        {
            ViewBag.CategoryList = db.BlogCategory.ToList();
            ViewBag.Status = _blogService.GetStatus();
            return View("BlogAndPage/CreateBlog");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlog(Blog blog)
        {
            try
            {
                _blogService.CreateBlog(blog, 1);
                return RedirectToAction("Blog");
            }
            catch
            {
                ViewBag.CategoryList = db.BlogCategory.ToList();
                ViewBag.Status = _blogService.GetStatus();
                return View("BlogAndPage/CreateBlog");
            }
        }
        public ActionResult EditBlog(int blogId)
        {
            var blog = db.Blog.Find(blogId);
            if (blog == null)
                return HttpNotFound();
            ViewBag.CategoryList = db.BlogCategory.ToList();
            ViewBag.Status = _blogService.GetStatus();
            return View("BlogAndPage/EditBlog", blog);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBlog(Blog blog)
        {
            try
            {
                _blogService.EditBlog(blog);
                return RedirectToAction("Blog");
            }
            catch
            {
                ViewBag.CategoryList = db.BlogCategory.ToList();
                ViewBag.Status = _blogService.GetStatus();
                return View("BlogAndPage/EditBlog", blog);
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
                result.message = "Thành công! Bài viết đã được xóa";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        public ActionResult LoadBlogCategory()
        {
            return PartialView("BlogAndPage/_BlogCategoryPartial", db.BlogCategory.ToList());
        }

        public ActionResult LoadEditBlogCategory(int blogCategoryId)
        {
            var model = db.BlogCategory.Find(blogCategoryId);
            return PartialView("BlogAndPage/_EditBlogCategoryPartial", model);
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
                result.message = "Thành công " + blogCategoryName + " đã được tạo";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
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
                result.message = "Thất bại! Tên chuyên mục vui lòng không để trống";
            }
            else
            {
                try
                {
                    _blogService.EditBlogCateogry(blogCategoryId, Name);
                    result.status = "success";
                    result.message = "Thành công! Danh mục đã được chỉnh sửa";
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
                result.message = "Thành công! Danh mục đã được xóa";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        public ActionResult Page(int? page)
        {
            var model = _blogService.GetBlogList("All", 2).OrderByDescending(a => a.DateCreated).ToPagedList(page ?? 1, 10);
            return View(model);
        }
        public ActionResult CreatePage()
        {
            ViewBag.Status = _blogService.GetStatus();
            return View("BlogAndPage/CreatePage");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePage(Blog page)
        {
            try
            {
                _blogService.CreateBlog(page, 2);
                return RedirectToAction("Page");
            }
            catch
            {
                ViewBag.Status = _blogService.GetStatus();
                return View("BlogAndPage/CreatePage");
            }
        }
        public ActionResult EditPage(int pageId)
        {
            var page = db.Blog.Find(pageId);
            if (page == null)
                return HttpNotFound();
            ViewBag.Status = _blogService.GetStatus();
            return View("BlogAndPage/EditPage", page);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPage(Blog page)
        {
            try
            {
                page.BlogCategoryId = null;
                _blogService.EditBlog(page);
                return RedirectToAction("Page");
            }
            catch
            {
                ViewBag.Status = _blogService.GetStatus();
                return View("BlogAndPage/EditPage", page);
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
                result.message = "Thành công! Trang đã được xóa";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult UpdateMenuCategory(int categoryId, bool IsShowMenu)
        {
            var result = new JsonStatusViewModel();
            try
            {
                var editItem = db.Category.Find(categoryId);
                editItem.IsShowMenu = IsShowMenu;
                db.Entry(editItem).State = EntityState.Modified;
                db.SaveChanges();
                result.status = "success";
                result.message = "Thành công! Dữ liệu đã được cập nhật";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult MenuTop()
        {
            return View();
        }
        public ActionResult LoadMenuList(int type)
        {
            var model = _informationService.GetMenuList(type);
            return PartialView("MenuTop/_MenuTopListPartial", model);
        }

        [HttpPost]
        public ActionResult AddMenu(Menu menu)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.AddMenu(menu);
                result.status = "success";
                result.message = "Thành công! Menu đã được tạo";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenu(Menu menu)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.EditMenu(menu);
                result.status = "success";
                result.message = "Thành công! Menu đã được chỉnh sửa";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult LoadEditMenu(int menuId)
        {
            try
            {
                var model = _informationService.GetMenu(menuId);
                return PartialView("MenuTop/_EditMenuTopPartial", model);
            }
            catch
            {
                return PartialView("_NullDataPartial");
            }
        }
        [HttpPost]
        public ActionResult DeleteMenu(int menuId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.DeleteMenu(menuId);
                result.status = "success";
                result.message = "Thành công! Đã xóa thành công";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Information()
        {
            var InformationCount = db.Information.ToList().Count;
            if (InformationCount == 0)
                _informationService.CreateInformation();
            return View();
        }

        public ActionResult LoadEditLogo()
        {
            ViewBag.Logo = db.Information.Find("Main").Logo;
            return PartialView("Information/_LogoPartial");
        }

        public ActionResult UpdateLogo(string logo)
        {
            var result = new JsonStatusViewModel();
            try
            {
                logo = string.IsNullOrEmpty(logo) ? "No_Picture.JPG" : logo;
                _informationService.UpdateLogo(logo);
                result.status = "success";
                result.message = "Thành công! Đã cập nhật Logo";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult LoadEditContact()
        {
            var model = _informationService.GetContact();
            return PartialView("Information/_ContactPartial", model);
        }
        public ActionResult UpdateContact(ContactViewModel contact)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.UpdateContact(contact);
                result.status = "success";
                result.message = "Thành công! Đã cập nhật Thông Tin Liên Lạc";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        public ActionResult LoadEditSocial()
        {
            var model = _informationService.GetSocial();
            return PartialView("Information/_SocialPartial", model);
        }
        public ActionResult UpdateSocial(SocialViewModel social)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.UpdateSocial(social);
                result.status = "success";
                result.message = "Thành công! Đã cập nhật Social Network";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng kiểm tra và thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult LoadMenuFooter(int type)
        {
            ViewBag.Type = type;
            var model = _informationService.GetMenuList(type);
            return PartialView("Information/_MenuFooterPartial", model);
        }
        public ActionResult LoadAddMenuFooterPartial(int type)
        {
            ViewBag.Type = type;
            return PartialView("Information/_AddMenuFooterPartial");
        }
        public ActionResult LoadEditMenuFooterPartial(int menuId)
        {
            var model = _informationService.GetMenu(menuId);
            return PartialView("Information/_EditMenuFooterPartial", model);
        }
        public ActionResult LoadEditSlider()
        {
            var model = _informationService.GetSliderImageList();
            return PartialView("Information/_EditSliderPartial", model);
        }
        public ActionResult LoadCreateSliderModal()
        {
            return PartialView("Information/_CreateSliderPartial");
        }
        public ActionResult AddImageToSlider(Slider slider)
        {
            var result = new JsonStatusViewModel();
            var image = db.Image.Find(slider.ImageId);
            if (image == null)
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            else
            {
                try
                {
                    _informationService.AddImageToSlider(slider);
                    result.status = "success";
                    result.message = "Thành công! Đã thêm slider";
                }
                catch
                {
                    result.status = "error";
                    result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
                }
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeleteSlider(int sliderId)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.DeleteSlider(sliderId);
                result.status = "success";
                result.message = "Thành công! Đã xóa slider";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        public ActionResult LoadProductHomePage()
        {
            var model = _productService.GetProductHomePage();
            ViewBag.Category = db.Category.Where(a => a.CategoryParentId == null).ToList();
            return PartialView("Information/_ProductHomePagePartial", model);
        }
        public ActionResult UpdateProductHomePage(List<ProductHomePage> productHomePageList)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _productService.UpdateProductHomePage(productHomePageList);
                result.status = "success";
                result.message = "Thành công! Đã cập nhật sản phẩm Trang Chủ";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult LoadFooterContact()
        {
            var model = _informationService.GetFooterContact();
            return PartialView("Information/_EditFooterContactPartial", model);
        }

        [ValidateInput(false)]
        public ActionResult UpdateFooterContact(string FooterContact, string FooterCopyright)
        {
            var result = new JsonStatusViewModel();
            try
            {
                _informationService.UpdateFooterContact(FooterContact, FooterCopyright);
                result.status = "success";
                result.message = "Thành công! Đã cập nhật Footer Contact";
            }
            catch
            {
                result.status = "error";
                result.message = "Thất bại! Có lỗi xảy ra, vui lòng thử lại";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Order(int? page)
        {
            var model = db.Order.Where(a => a.Status != 4).OrderByDescending(a => a.DateCreated).ToPagedList(page ?? 1, 20);
            return View(model);
        }
        public ActionResult OrderDetails(string OrderId)
        {
            var model = db.OrderDetails.Where(a => a.OrderId == OrderId).ToList();
            return View("Order/OrderDetails", model);
        }
    }
}
