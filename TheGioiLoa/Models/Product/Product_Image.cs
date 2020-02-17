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
        public string ImageId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public bool IsMain { get; set; }

        public virtual Product Product { get; set; }
    }
}
