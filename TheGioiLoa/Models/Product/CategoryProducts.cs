namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryProducts")]
    public partial class CategoryProducts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryProducts()
        {
           
        }
        [Key, Column(Order = 1)]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}