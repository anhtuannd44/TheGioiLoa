using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Controllers
{
    public class CartController : Controller
    {
        private readonly HelperFunction _hepler = new HelperFunction();
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly ApplicationDbContext dbApp = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = ShoppingCart.Cart;
            return View(cart.Items);
        }


        public ActionResult LoadCartDetails()
        {
            var cart = ShoppingCart.Cart;
            return PartialView("_CartDetailPartial", cart.Items);
        }


        public ActionResult LoadCartTotal()
        {
            var cart = ShoppingCart.Cart;
            return PartialView("_CartTotalPartial", cart.Items);
        }

        [HttpPost]
        public ActionResult LoadCartBtnAction()
        {
            return PartialView("_CartBtnActionPartial");
        }

        [Authorize]
        public ActionResult CheckOut()
        {
            var cart = ShoppingCart.Cart;
            if (cart.Count == 0)
                return RedirectToAction("Index");
            var userId = User.Identity.GetUserId();
            var user = dbApp.Users.Find(userId);
            var model = new Order()
            {
                UserAddress = user.Address,
                UserId = user.Id,
                UserName = user.FullName,
                UserEmail = user.Email,
                UserPhone = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(Order info)
        {
            var cart = ShoppingCart.Cart;
            if (cart.Count == 0)
                return RedirectToAction("NoItemInCart");
            try
            {
                var userId = User.Identity.GetUserId();
                if (userId == info.UserId)
                {
                    info.OrderId = _hepler.RandomString();
                    info.DateCreated = DateTime.Now;
                    info.Status = 1;
                    db.Order.Add(info);

                    IList<OrderDetails> listDetails = new List<OrderDetails>();
                    foreach (var item in cart.Items)
                    {
                        var addDetail = new OrderDetails()
                        {
                            ProductId = item.ProductId,
                            Count = item.Count,
                            Price = item.Price,
                            SalePrice = item.PriceSale,
                            OrderId = info.OrderId
                        };
                        db.OrderDetails.Add(addDetail);
                    }
                    db.SaveChanges();
                    ShoppingCart.Cart.Clear();

                    return RedirectToAction("OrderSuccess", new { orderid = info.OrderId });
                }
            }
            catch
            {
            }
            return RedirectToAction("OrderFailed");
        }

        [Authorize]
        public ActionResult OrderFailed()
        {
            return View();
        }

        [Authorize]
        public ActionResult OrderSuccess(string orderid)
        {
            var userId = User.Identity.GetUserId();
            var order = db.Order.Find(orderid);
            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (string.IsNullOrEmpty(orderid) || order.UserId != userId)
                return RedirectToAction("Index", "Home");

            var model = new OrderViewModel
            {
                Order = order
            };

            var orderItem = db.OrderDetails.Where(a => a.OrderId == orderid);
            var orderDetailsList = new List<OrderDeltailViewModel>();
            foreach (var item in orderItem)
            {

                var orderDetail = new OrderDeltailViewModel()
                {
                    Product = db.Product.Find(item.ProductId),
                    Price = item.Price == null ? 0 : item.Price,
                    PriceSale = item.SalePrice == null ? item.Price == null ? 0 : item.Price : item.SalePrice,
                    Count = item.Count,
                    Guarantee = item.Guarantee

                };
                orderDetailsList.Add(orderDetail);
            }
            model.OrderDetails = orderDetailsList;
            return View(model);
        }
        public ActionResult NoItemInCart()
        {
            return View();
        }

        [HttpPost]
        public void Add(int productId)
        {
            var cart = ShoppingCart.Cart;
            cart.Add(productId);
        }

        [HttpPost]
        public void Remove(int productId)
        {
            var cart = ShoppingCart.Cart;
            cart.Remove(productId);
        }

        [HttpPost]
        public void Update(int productId, bool isCount)
        {
            var cart = ShoppingCart.Cart;
            cart.Update(productId, isCount);
        }

        [HttpPost]
        public ActionResult Clear()
        {
            var cart = ShoppingCart.Cart;
            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}