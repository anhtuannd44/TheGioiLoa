using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGioiLoa.Models.ViewModel
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderDeltailViewModel> OrderDetails { get; set; }

    }
    public class OrderDeltailViewModel
    {
        public Product Product { get; set; }

        public string OrderId { get; set; }


        public double? Price { get; set; }

        public double? PriceSale { get; set; }

        public int Guarantee { get; set; }

        [Required]
        public int Count { get; set; }
    }

}