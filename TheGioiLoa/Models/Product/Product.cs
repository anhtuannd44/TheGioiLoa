using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace TheGioiLoa.Models
{
    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Product_Images = new HashSet<Product_Images>();
            CategoryProduct = new HashSet<CategoryProducts>();
            Product_Tag = new HashSet<Product_Tag>();
            Review = new HashSet<Review>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        [ForeignKey("OrderDetails")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        [StringLength(500)]
        public string Name { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Services { get; set; }

        [Display(Name = "Giá sản phẩm")]
        public double? Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public double? PriceSale { get; set; }

        //Cover of Product
        public string ImageId { get; set; }

        [Display(Name="Thương Hiệu")]
        public int? BrandId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public int Status { get; set; }


        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Promotion { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Contents { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Specifications { get; set; }


        public int Guarantee { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Image Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryProducts> CategoryProduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Tag> Product_Tag { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Images> Product_Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
