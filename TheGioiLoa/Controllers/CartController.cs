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
        private HelperFunction _hepler = new HelperFunction();
        private TheGioiLoaModel db = new TheGioiLoaModel();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = ShoppingCart.Cart;
            return View(cart.Items);
        }

        [HttpPost]
        public ActionResult LoadCartDetails()
        {
            var cart = ShoppingCart.Cart;
            return PartialView("_CartDetailPartial", cart.Items);
        }

        [HttpPost]
        public ActionResult LoadCartTotal()
        {
            var cart = ShoppingCart.Cart;
            return PartialView("_CartTotalPartial", cart.Items);
        }
        public ActionResult CheckOut()
        {
            var cart = ShoppingCart.Cart;
            if (cart.Count == 0)
                return RedirectToAction("NoItemInCart");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderInformationViewModel info)
        {
            var cart = ShoppingCart.Cart;
            if (cart.Count == 0)
                return RedirectToAction("NoItemInCart");
            if (ModelState.IsValid)
            {
                var newOrderId = _hepler.RandomString();
                var addOrder = new Order()
                {
                    OrderId = newOrderId,
                    DateCreated = DateTime.Now,
                    UserName = info.UserName,
                    UserPhone = info.UserPhone,
                    UserEmail = info.UserEmail,
                    Note = info.Note,
                    UserAddress = info.UserAddrress,
                    DateModified = DateTime.Now,
                    PaymentMethod = 1,
                    Status = 1
                };
                db.Order.Add(addOrder);


                IList<OrderDetails> listDetails = new List<OrderDetails>();
                foreach (var item in cart.Items)
                {
                    var addDetail = new OrderDetails()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        Price = item.Price,
                        SalePrice = item.PriceSale,
                        OrderId = newOrderId
                    };
                    db.OrderDetails.Add(addDetail);
                }
                db.SaveChanges();
                cart = null;
                return RedirectToAction("OrderSuccess", new { orderid = newOrderId });
            }
            return View(info);
        }

        public ActionResult OrderSuccess(string orderid)
        {
            if (string.IsNullOrEmpty(orderid))
                return RedirectToAction("Index", "Home");
            var order = db.Order.Find(orderid);
            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                    Price = item.Price,
                    PriceSale = item.SalePrice,
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