using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TheGioiLoa.Models.ViewModel
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderDeltailViewModel> OrderDetails { get; set; }
        public double TotalPrice
        {
            get {
                return OrderDetails.Sum(p => p.Price == null ? 0 : (double)p.Price * p.Count);
            }
        }
        public double TotalSalePrice
        {
            get
            {
                return OrderDetails.Sum(p => (p.PriceSale == null ? (p.Price == null ? 0 : (double)p.Price) : (double)p.PriceSale) * p.Count);
            }
        }
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