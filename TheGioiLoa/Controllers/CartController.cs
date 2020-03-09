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

        public ActionResult CheckOut()
        {
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
                        Price = item.ListedPrice,
                        SalePrice = item.Price,
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
            order.OrderDetails = db.OrderDetails.Where(a => a.OrderId == orderid).ToList();

            return View(order);
        }
        public ActionResult NoItemInCart()
        {
            return View();
        }

        public ActionResult Add(int productId)
        {
            var cart = ShoppingCart.Cart;
            cart.Add(productId);

            var info = new
            {
                cart.Count,
                cart.TotalPrice,
                cart.TotalListedPrice
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(int productId)
        {
            var cart = ShoppingCart.Cart;
            cart.Remove(productId);

            var info = new
            {
                cart.Count,
                cart.TotalPrice,
                cart.TotalListedPrice,
                cartDiscount = cart.TotalListedPrice - cart.TotalPrice
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(int productId, bool isCount)
        {
            var cart = ShoppingCart.Cart;
            cart.Update(productId, isCount);

            var p = cart.Items.Single(i => i.ProductId == productId);
            var info = new
            {
                newItemTotalPrice = p.Count * p.Price,
                newItemTotalListedPrice = p.Count * p.ListedPrice,
                cart.Count,
                cart.TotalPrice,
                cart.TotalListedPrice,
                cartDiscount = cart.TotalListedPrice - cart.TotalPrice
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clear()
        {
            var cart = ShoppingCart.Cart;
            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}