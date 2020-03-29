namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public string OrderId { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Họ và tên")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Số điện thoại")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ giao hàng")]
        public string UserAddress { get; set; }

        public string Note { get; set; }

        [Required]
        public string UserId { get; set; }

        public int PaymentMethod { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        //1-Processing 2-Completed 3-Removed
        public int Status { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public double TotalPrice
        {
            get
            {
                return OrderDetails.Sum(p => p.Price == null ? 0 : (double)p.Price * p.Count);
            }
        }
        public double TotalSalePrice
        {
            get
            {
                return OrderDetails.Sum(p => (p.SalePrice == null ? (p.Price == null ? 0 : (double)p.Price) : (double)p.SalePrice) * p.Count);
            }
        }

    }
}
