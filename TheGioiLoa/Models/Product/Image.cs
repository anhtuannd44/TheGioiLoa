namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Image")]
    public partial class Image
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Image()
        {
            Product_Images = new HashSet<Product_Images>();
            Slider = new HashSet<Slider>();
            Product = new HashSet<Product>();
            Blog = new HashSet<Blog>();
        }

        [Key]
        [ForeignKey("Product")]
        public string ImageId { get; set; }

        public DateTime DateCreated { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Images> Product_Images { get; set; }
        public virtual ICollection<Slider> Slider { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Blog> Blog { get; set; }
    }
}
