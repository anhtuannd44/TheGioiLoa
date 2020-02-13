namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Image
    {
        [Key]
        [StringLength(50)]
        public string ImageId { get; set; }

        [Required]
        [StringLength(300)]
        public string ProductId { get; set; }

        public bool IsMain { get; set; }

        public virtual Product Product { get; set; }
    }
}
