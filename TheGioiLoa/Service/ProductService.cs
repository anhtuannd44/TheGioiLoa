using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        public void CreateCategory(CreateCategoryViewModel category)
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
        }
        public void EditCategory(EditCategoryViewModel category)
        {
            var editItem = db.Category.Find(category.CategoryId);
            editItem.DateModified = DateTime.Now;
            editItem.Name = category.Name;
            editItem.Url = _helper.CreateUrl(category.Name);

            db.Entry(editItem).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void RemoveCategory(int categoryId)
        {
            var removeItem = db.Category.Find(categoryId);
            db.Category.Remove(removeItem);
            db.CategoryProduct.RemoveRange(db.CategoryProduct.Where(a => a.CategoryId == categoryId));

            var childCategoryList = db.Category.Where(a => a.CategoryParentId == categoryId);
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
        }

        //SortBy: "Newest" - "PriceDes" - "Price"
        public List<ProductViewModel> GetProductList()
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
                    PriceSale = item.PriceSale,
                    Price = item.Price,
                    Status = item.Status,
                    Name = item.Name,
                    Url = item.Url
                };
                var Categories = db.CategoryProduct.Include(a => a.Category).Where(a => a.ProductId == item.ProductId).ToList();
                if (Categories != null)
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
                    addProduct.Categories = addListCategory;
                }
                var Images = db.Product_Image.Where(a => a.ProductId == item.ProductId).ToList();
                if (Images != null)
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
            return model;
        }
        public ProductViewModel GetAllInformationOfProductItem(Product product)
        {
            try
            {
                var model = new ProductViewModel()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Url = _helper.CreateUrl(product.Name),
                    Description = product.Description,
                    DateModified = DateTime.Now,
                    Price = product.Price,
                    PriceSale = product.PriceSale,
                    Status = product.Status,
                    Characteristics = product.Characteristics,
                    Promotion = product.Promotion,
                    Videos = product.Videos,
                    Details = product.Details,
                    CoverName = product.Cover,
                    Guarantee = product.Guarantee
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
                        IsChecked = (db.CategoryProduct.Find(item.CategoryId, product.ProductId) != null) ? true : false
                    });
                }
                model.Categories = addCategoryList.ToList();

                var image = db.Product_Image.Include(a => a.Image).Where(a => a.ProductId == product.ProductId).ToList();
                var imageList = "";
                foreach (var item in image)
                {
                    imageList += item.ImageId + ",";
                }
                model.Image = imageList;

                var brand = db.Brand.ToList();
                var addBrandList = new List<BrandSelectedViewModel>();
                foreach (var item in brand)
                {
                    addBrandList.Add(new BrandSelectedViewModel()
                    {
                        BrandId = item.BrandId,
                        Name = item.Name,
                        IsChecked = (db.Product.Find(product.ProductId).BrandId == item.BrandId) ? true : false
                    });
                }
                model.Brand = addBrandList.ToList();
                var tag = db.Product_Tag.Include(a => a.Tag).Where(a => a.ProductId == product.ProductId);
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
                model.StatusList = GetStatus();
                model.IsGetDataSuccess = true;
                return model;
            }
            catch
            {
                var model = new ProductViewModel()
                { IsGetDataSuccess = false };
                return model;
            }

        }

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
                PriceSale = product.PriceSale,
                Status = product.Status,
                Characteristics = product.Characteristics,
                Promotion = product.Promotion,
                Videos = product.Videos,
                Details = product.Details,
                Cover = product.CoverName,
                Guarantee = product.Guarantee
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
                var imageListArray = imageList.Split(',');
                foreach (var item in imageListArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        db.Product_Image.Add(new Product_Image()
                        {
                            ImageId = item,
                            ProductId = productId
                        });
                    }
                }
                db.SaveChanges();
            }
        }

        public void AddTagToProduct(int productId, string tagArray)
        {
            if (!string.IsNullOrEmpty(tagArray))
            {
                var tagList = tagArray.Split('|');
                foreach (var item in tagList)
                {
                    int myInt;
                    db.Product_Tag.Add(new Product_Tag()
                    {
                        ProductId = productId,
                        TagId = (int.TryParse(item, out myInt)) ? myInt : 0
                    });
                }
                db.SaveChanges();
            }
        }

        public void RemoveCategoryProduct(int productId)
        {
            var removeList = db.CategoryProduct.Where(a => a.ProductId == productId);
            if (removeList != null)
                db.CategoryProduct.RemoveRange(removeList);
        }

        public void RemoveProductImage(int productId)
        {
            var removeList = db.Product_Image.Where(a => a.ProductId == productId);
            if (removeList != null)
                db.Product_Image.RemoveRange(removeList);
        }
        public void RemoveProductTag(int productId)
        {
            var removeList = db.Product_Tag.Where(a => a.ProductId == productId);
            if (removeList != null)
                db.Product_Tag.RemoveRange(removeList);
        }
        public void EditProductDb(ProductViewModel product)
        {
            var addProduct = db.Product.Find(product.ProductId);

            addProduct.Name = product.Name;
            addProduct.Url = _helper.CreateUrl(product.Name);
            addProduct.BrandId = product.BrandId;
            addProduct.Description = product.Description;
            addProduct.DateModified = DateTime.Now;
            addProduct.Price = product.Price;
            addProduct.PriceSale = product.PriceSale;
            addProduct.Status = product.Status;
            addProduct.Characteristics = product.Characteristics;
            addProduct.Promotion = product.Promotion;
            addProduct.Videos = product.Videos;
            addProduct.Details = product.Details;
            addProduct.Cover = !string.IsNullOrEmpty(product.CoverName) ? product.CoverName : "No_Picture.JPG";
            addProduct.Guarantee = product.Guarantee;

            db.Entry(addProduct).State = EntityState.Modified;
            db.SaveChanges();
        }
        public List<ProductHomePage> GetProductHomePage()
        {
            var productHomePage = db.ProductHomePage.Count();
            if (productHomePage == 0)
            {
                CreateProductHomePage();
            }
            else if (productHomePage != 10)
            {
                RemoveAllProductHomePage();
                CreateProductHomePage();
            }
            return db.ProductHomePage.ToList();
        }
        public void RemoveAllProductHomePage()
        {
            db.ProductHomePage.RemoveRange(db.ProductHomePage);
            db.SaveChanges();
        }
        public void CreateProductHomePage()
        {
            for (int i = 1; i <= 10; i++)
            {
                var addItem = new ProductHomePage()
                {
                    Id = i.ToString(),
                    Position = i,
                    CategoryId = null,
                    IsShow = false,
                    YoutubeLink = null
                };
                db.ProductHomePage.Add(addItem);
            }
            db.SaveChanges();
        }
        public ProductHomePageViewModel GetProductHomePageList(int id)
        {
            var productHomePage = db.ProductHomePage.Count();
            if (productHomePage == 0)
            {
                CreateProductHomePage();
            }
            else if (productHomePage != 10)
            {
                RemoveAllProductHomePage();
                CreateProductHomePage();
            }

            var result = new ProductHomePageViewModel();
            var productHomePageItem = db.ProductHomePage.Find(id.ToString());

            if (productHomePageItem.CategoryId != null)
            {
                var count = 10;
                if (id == 2)
                    count = 8;
                else if (id == 6)
                    count = 6;
                var categoryProductList = db.CategoryProduct.Where(a => a.CategoryId == productHomePageItem.CategoryId).Take(count).ToList();
                if (categoryProductList.Count != 0)
                {
                    var productList = new List<Product>();
                    foreach (var item in categoryProductList)
                    {
                        productList.Add(item.Product);
                    }
                    result.Product = productList;

                    var categoryList = new List<Category>();
                    var categoryId = categoryProductList.FirstOrDefault().CategoryId;
                    var childCategory = db.Category.Where(a => a.CategoryParentId == categoryId).ToList();
                    if (childCategory.Count != 0)
                        foreach (var item in childCategory)
                        {
                            categoryList.Add(item);
                            var subCategory = db.Category.Where(a => a.CategoryParentId == item.CategoryId).ToList();
                            if (subCategory.Count != 0)
                                foreach (var subItem in subCategory)
                                {
                                    categoryList.Add(subItem);
                                }
                        }
                    result.CategoryList = categoryList;
                    result.Category = db.Category.Find(categoryId);
                    result.Count = count;
                    result.Id = id;
                }
            }
            return result;
        }
        public string GetYoutubeLink()
        {
            var link = db.ProductHomePage.Find(6.ToString()).YoutubeLink;
            if (!string.IsNullOrEmpty(link))
                link = _helper.GetYoutubeVideoId(link);
            return link;
        }
        public void UpdateProductHomePage(List<ProductHomePage> productHomePageList)
        {
            foreach (var item in productHomePageList)
            {
                db.Entry(item).State = EntityState.Modified;
            }
            db.SaveChanges();
        }
        public List<StatusEnum> GetStatus()
        {
            var statusList = new List<StatusEnum>(){
            new StatusEnum(){StatusId = 1, Name = "Công bố"},
            new StatusEnum(){StatusId = 2, Name = "Chưa đăng"},
            new StatusEnum(){StatusId = 3, Name = "Hết hàng"},
            new StatusEnum(){StatusId = 4, Name = "Đưa vào thùng rác"}
            };
            return statusList;
        }

        public List<Product> GetProductLists(int? categoryId, string sortBy, int? priceSortFrom, int? priceSortTo, int? take)
        {
            var productLists = new List<Product>();
            if (categoryId == null)
            {
                productLists = db.Product.Where(a => a.Status == 1 || a.Status == 3).ToList();
            }
            else
            {
                var productIdList = db.CategoryProduct.Where(a => a.CategoryId == categoryId).ToList();
                foreach (var item in productIdList)
                {
                    var addProduct = db.Product.Find(item.ProductId);
                    if ((addProduct.Status == 1 || addProduct.Status == 3))
                        productLists.Add(addProduct);
                }
            }
            if (priceSortFrom == 0)
                productLists = productLists.Where(a => a.Price <= priceSortTo || a.Price == null).ToList();
            else if (priceSortFrom !=null)
                productLists = productLists.Where(a => a.PriceSale >= priceSortFrom && a.PriceSale <= priceSortTo).ToList();

            switch (sortBy)
            {
                case "newest":
                    productLists = productLists.OrderByDescending(a => a.DateCreated).ToList();
                    break;
                case "priceLowToHigh":
                    productLists = productLists.OrderBy(a => a.Price).ToList();
                    break;
                case "priceHighToLow":
                    productLists = productLists.OrderByDescending(a => a.Price).ToList();
                    break;
                case "nameAsc":
                    productLists = productLists.OrderBy(a => a.Price).ToList();
                    break;
            }
            if (take != null)
            {
                productLists = productLists.Take(take.Value).ToList();
            }
            return productLists;
        }
    }
}