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

        public int ProductId { get; set; }

        public int IsMain { get; set; }

        public virtual Product Product { get; set; }
    }
}
