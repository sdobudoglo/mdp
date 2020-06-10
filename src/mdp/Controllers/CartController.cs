using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mdp.Models;

namespace mdp.Controllers
{
    public class CartController : Controller
    {
        public const string CartKey = "CartId";

        public PurchaseModel DBContext
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PurchaseModel>();
            }
        }

        private string CartKeyValue()
        {
            var cart_key = HttpContext.Request.Cookies[CartKey];
            if (cart_key == null || string.IsNullOrEmpty(cart_key.Value))
            {
                // generate unique id for carts identification
                Guid tempCartId = Guid.NewGuid();
                cart_key = new HttpCookie(CartKey, tempCartId.ToString());
                HttpContext.Response.Cookies.Add(cart_key);
            }
            return cart_key.Value;
        }

        // GET: Cart
        public ActionResult AddToCart(int product_id, string product_name)
        {
            var cart_key_value = CartKeyValue();

            var cart_item = DBContext.Carts.SingleOrDefault(p => p.ProductId == product_id
                                                              && p.CartId == cart_key_value);
            if (cart_item == null)
            {
                cart_item = new Cart
                {
                    CartId = cart_key_value,
                    ProductId = product_id,
                    ProductName = product_name,
                    Amount = 1
                };
                DBContext.Carts.Add(cart_item);
            }
            else
            {
                cart_item.Amount++;
            }
            DBContext.SaveChanges();

            return RedirectToAction("Cart");
        }

        public ActionResult RemoveFromCart()
        {
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            var cart_key_value = CartKeyValue();
            return View(DBContext.Carts.Where(p => p.CartId == cart_key_value).ToList());
        }

        public ActionResult MakeOrder()
        {
            return View();
        }

        private void ClearCart()
        {

        }
    }
}