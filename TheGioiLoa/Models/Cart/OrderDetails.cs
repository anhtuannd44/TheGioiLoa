namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("OrderDetails")]
    public partial class OrderDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderDetails()
        {

        }

        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]

        public int ProductId { get; set; }


        public double? Price { get; set; }

        public double? SalePrice { get; set; }

        public int Guarantee { get; set; }

        [Required]
        public int Count { get; set; }


        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public double TotalPrice
        {
            get
            {
                return ((SalePrice == null ? (Price == null ? 0 : (double)Price) : (double)SalePrice)) * Count;
            }
        }

    }
}
