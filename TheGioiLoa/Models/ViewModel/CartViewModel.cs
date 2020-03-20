using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class CartViewModel
    {
        public int ProductId { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public string Cover { get; set; }

        public double? PriceSale { get; set; }
        public int Count { get; set; }
    }
    
    public class OrderInformationViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập tên của mình")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập số điện thoại")]
        [Phone(ErrorMessage ="Chưa đúng định dạng số điện thoại")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ của mình")]
        public string UserAddress { get; set; }

        public string Note { get; set; }
    }
}