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
            Product_Image = new HashSet<Product_Image>();
            CategoryProduct = new HashSet<CategoryProducts>();
            Product_Tag = new HashSet<Product_Tag>();
            Product_Image = new HashSet<Product_Image>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public double? Price { get; set; }
        public string Cover { get; set; }

        public int? BrandId { get; set; }

        public double? ListedPrice { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int Status { get; set; }

        [AllowHtml]
        [StringLength(100)]
        public string Promotion { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Characteristics { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Details { get; set; }

        [AllowHtml]
        [StringLength(300)]
        public string Videos { get; set; }


        public virtual Brand Brand { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryProducts> CategoryProduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Tag> Product_Tag { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Image> Product_Image { get; set; }
    }
}
