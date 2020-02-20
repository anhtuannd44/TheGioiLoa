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
        [Required(ErrorMessage ="Bạn chưa nhập tên sản phẩm")]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name="Mô tả ngắn")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Display(Name = "Thương hiệu")]
        public int? BrandId { get; set; }

        [Display(Name = "Giá bán")]
        public double? Price { get; set; }

        [Display(Name = "Giá niêm yết")]
        public double? ListedPrice { get; set; }

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
        public string Video { get; set; }

        [Display(Name = "Trạng thái sản phẩm")]
        public int Status { get; set; }

        public string Tag { get; set; }
    }

    public class CreateProductActionViewModel
    {

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Display(Name = "Giá bán")]
        public double? Price { get; set; }

        [Display(Name = "Giá niêm yết")]
        public double? ListedPrice { get; set; }

        [Display(Name = "Trạng thái sản phẩm")]
        public int Status { get; set; }

        [Display(Name = "Thương hiệu")]
        public int? BrandId { get; set; }
    }

}