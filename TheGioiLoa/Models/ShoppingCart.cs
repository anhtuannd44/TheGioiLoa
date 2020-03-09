using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Models
{
    public class ShoppingCart
    {
        // Lấy giỏ hàng từ Session
        public static ShoppingCart Cart
        {
            get
            {
                var cart = HttpContext.Current.Session["Cart"] as ShoppingCart;
                // Nếu chưa có giỏ hàng trong session -> tạo mới và lưu vào session
                if (cart == null)
                {
                    cart = new ShoppingCart();
                    HttpContext.Current.Session["Cart"] = cart;
                }
                return cart;
            }
        }
        
        // Chứa các mặt hàng đã chọn

        public List<CartViewModel> Items = new List<CartViewModel>();

        public void Add(int productId)
        {
            try // tìm thấy trong giỏ -> tăng số lượng lên 1
            {
                var item = Items.Single(i => i.ProductId == productId);
                item.Count++;
            }
            catch // chưa có trong giỏ -> truy vấn CSDL và bỏ vào giỏ
            {
                var db = new TheGioiLoaModel();
                var item = db.Product.Find(productId);
                Items.Add(new CartViewModel()
                {
                    Name = item.Name,
                    Url = item.Url,
                    ProductId = item.ProductId,
                    Cover = item.Cover,
                    Price = (item.Price == null) ? 0 : (double)item.Price,
                    ListedPrice = (item.ListedPrice == null) ? 0 : (double)item.ListedPrice,
                    Count = 1
                });
            }
        }

        public void Remove(int productId)
        {
            var item = Items.Single(i => i.ProductId == productId);
            Items.Remove(item);
        }

        public void Update(int productId, bool IsPlus)
        {
            var item = Items.Single(i => i.ProductId == productId);
            if (IsPlus)
            {
                item.Count++;
            }
            else
            {
                if (item.Count == 1)
                    Remove(productId);
                else
                    item.Count--;
            }

        }

        public void Clear()
        {
            Items.Clear();
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public double TotalPrice
        {
            get
            {
                return Items.Sum(p => p.Price * p.Count);
            }
        }
        public double TotalListedPrice
        {
            get
            {
                return Items.Sum(p => p.ListedPrice * p.Count);
            }
        }
    }
}