using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class CreateProductViewModel
    {
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Display(Name = "Thương hiệu")]
        public int? BrandId { get; set; }

        [Display(Name = "Giá bán")]
        public double? Price { get; set; }

        [Display(Name = "Giá đã giảm")]
        public double? PriceSale { get; set; }

        [AllowHtml]
        [Display(Name = "Đặc điểm nổi bật")]
        [Column(TypeName = "ntext")]
        public string Characteristics { get; set; }

        [AllowHtml]
        [Display(Name = "Thông số kỹ thuật")]
        [Column(TypeName = "ntext")]
        public string Details { get; set; }

        [AllowHtml]
        [Display(Name = "Khuyến mãi")]
        public string Promotion { get; set; }

        [AllowHtml]
        [Display(Name = "Video")]
        public string Videos { get; set; }

        [Display(Name = "Trạng thái sản phẩm")]
        public int Status { get; set; }

        public int Guarantee { get; set; }

        public string Tag { get; set; }

        public string Image { get; set; }
        public string CoverName { get; set; }
        public HttpPostedFileBase Cover { get; set; }
    }

    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        [StringLength(500)]
        public string Name { get; set; }
        public string Url { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int? BrandId { get; set; }

        [Display(Name = "Giá bán")]
        public double? Price { get; set; }

        [Display(Name = "Giá đã giảm")]
        public double? PriceSale { get; set; }

        public DateTime DateModified { get; set; }

        [AllowHtml]
        [Display(Name = "Đặc điểm nổi bật")]
        [Column(TypeName = "ntext")]
        public string Characteristics { get; set; }

        [AllowHtml]
        [Display(Name = "Thông số kỹ thuật")]
        [Column(TypeName = "ntext")]
        public string Details { get; set; }

        [AllowHtml]
        [Display(Name = "Khuyến mãi")]
        public string Promotion { get; set; }

        [AllowHtml]
        [Display(Name = "Video")]
        public string Videos { get; set; }

        [Display(Name = "Trạng thái sản phẩm")]
        public int Status { get; set; }
        public int Guarantee { get; set; }

        public string CoverName { get; set; }

        public string Tag { get; set; }

        public string Image { get; set; }

        public string Breadcrumb { get; set; }

        public List<Tag> Tags { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public List<CategoryProductEditViewModel> Categories { get; set; }
        public List<BrandSelectedViewModel> Brand { get; set; }
        public Brand Brands { get; set; }
        public List<string> ImageList { get; set; }
        public List<StatusEnum> StatusList { get; set; }
        public List<Product> ProductRelateds { get; set; }
        public List<Product> ProductAccessories { get; set; }
        public ReviewViewModel Review { get; set; }
        public bool IsGetDataSuccess { get; set; }
    }

    public class ProductHomePageViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public List<Product> Product { get; set; }
        public Category Category { get; set; }
        public List<Category> CategoryList { get; set; }
        public string LinkYoutube { get; set; }
    }

    public class ReviewViewModel
    {
        public List<EachReviewViewModel> EachReviewViewModel { get; set; }
        public double AvgStar { get; set; }
        public int CommentCount { get; set; }
    }
   
    public class EachReviewViewModel
    {
        public int Star { get; set; }
        public double Percent { get; set; }
        public int Count { get; set; }
    }
    public class CategoryProductEditViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int? CategoryParentId { get; set; }

        public bool IsChecked { get; set; }
    }

    public class UploadImageViewModel
    {
        public string status { get; set; }
        public string path { get; set; }
        public string message { get; set; }
    }

    public class StatusEnum
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
    }

    public class TagViewModel
    {
        public string TagId { get; set; }
        public string Name { get; set; }
    }
}