namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Images
    {
        [Key, Column(Order = 1)]
        public string ImageId { get; set; }

        [Key, Column(Order = 2)]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Image Image { get; set; }
    }
}
