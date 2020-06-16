using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Net.Mail;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mdp.Models;

namespace mdp.Controllers
{
    public class CartController : Controller
    {
        public const string CartKey = "CartId";
        public const string SMTPAccountName = "mdp17062020@gmail.com";
        public const string SMTPAccountPassword = "Dkfglg1969";

        public PurchaseModel DBContext
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PurchaseModel>();
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
        public ActionResult AddToCart(int product_id, string product_name, int amount = 1)
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
                    Amount = amount
                };
                DBContext.Carts.Add(cart_item);
            }
            else
            {
                cart_item.Amount += amount;
            }
            DBContext.SaveChanges();

            return RedirectToAction("Cart");
        }

        public ActionResult RemoveFromCart(int product_id, bool all_amount = false)
        {
            var cart_key_value = CartKeyValue();

            var cart_item = DBContext.Carts.SingleOrDefault(p => p.ProductId == product_id
                                                              && p.CartId == cart_key_value);
            if (cart_item != null)
            {
                if (cart_item.Amount == 1 || all_amount)
                {
                    DBContext.Carts.Remove(cart_item);
                } else
                {
                    cart_item.Amount--;
                }

                DBContext.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            var cart_key_value = CartKeyValue();
            return View(DBContext.Carts.Where(p => p.CartId == cart_key_value).ToList());
        }

        public ActionResult MakeOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                return View(user);
            }

            return View();
        }

        public ActionResult ValidateOrder(OrderViewModel data)
        {
            var cart_key_value = CartKeyValue();
            var carts = DBContext.Carts.Where(p => p.CartId == cart_key_value);

            var purchase = new Purchase
            {
                UserId = data.UserId,
                DelivType = data.Delivery,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                UserName = data.Name,
                UserSurname = data.Surname,
                DelivAddress = (data.Delivery == DeliveryType.Courier || data.Delivery == DeliveryType.Post) ? data.Address : ""
            };

            foreach (var cart in carts) {
                var order = new Order { ProductId = cart.ProductId, ProductAmount = cart.Amount };
                purchase.Orders.Add(order);
                DBContext.Orders.Add(order);
            }

            DBContext.Purchases.Add(purchase);
            DBContext.SaveChanges();

            // send mails with purchase information for the shop's manager
            string manager_mail_text = "Поступил заказ номер: " + purchase.Id + "\nНа имя: " + data.Surname + " " + data.Name + " " + data.Patronymic;
            manager_mail_text += "\nСостав заказа:\n";
            foreach (var item in carts)
            {
                manager_mail_text += item.ProductName + " в количестве " + item.Amount + "\n";
            }
            manager_mail_text += "Доставка: ";
            switch (data.Delivery)
            {
                case DeliveryType.Courier:
                    manager_mail_text += "курьером по адресу:\n" + data.Address;
                    break;
                case DeliveryType.Post:
                    manager_mail_text += "почтой, почтовый индекс:\n" + data.Address;
                    break;
                case DeliveryType.Self:
                    manager_mail_text += "самовывозом из нашего магазина по адресу: \nРашпилевская 142";
                    break;
            }
            manager_mail_text += "\nТелефон для связи: " + data.PhoneNumber;
            SendEmail(SMTPAccountName, "Заказ в магазине робототехники", manager_mail_text);

            // send mails with purchase information for the user
            string cust_mail_text = "Здравствуйте " + data.Surname + " " + data.Name + " " + data.Patronymic + "\n\nВаш номер заказа: " + purchase.Id + "\nВы заказали:\n";
            
            foreach (var item in carts)
            {
                cust_mail_text += item.ProductName + " в количестве " + item.Amount + "\n";
            }
            cust_mail_text += "Вы выбрали доставку ";
            switch (data.Delivery)
            {
                case DeliveryType.Courier:
                    cust_mail_text += "курьером по адресу:\n" + data.Address;
                    break;
                case DeliveryType.Post:
                    cust_mail_text += "почтой, почтовый индекс:\n" + data.Address;
                    break;
                case DeliveryType.Self:
                    cust_mail_text += "самовывозом из нашего магазина по адресу: \nРашпилевская 142";
                    break;
            }
            cust_mail_text += "\nВы указали номер телефона для связи: " + data.PhoneNumber;

            SendEmail(purchase.Email, "Заказ в магазине робототехники", cust_mail_text);

            // clear the carts as far we have stored purchase information to the database
            ClearCart();

            return View();
        }

        private void ClearCart()
        {
            var cart_key_value = CartKeyValue();
            var to_delete = DBContext.Carts.Where(p => p.CartId == cart_key_value);

            foreach (var item in to_delete)
            {
                DBContext.Carts.Remove(item);
            }
            DBContext.SaveChanges();
        }

        private void SendEmail(string email, string subject, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(SMTPAccountName, "Магазин робототехники", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(SMTPAccountName, SMTPAccountPassword);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateCopyMessage(): {0}",
                    ex.ToString());
            }
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            int count = 0;
            var cart_key_value = CartKeyValue();

            var carts = DBContext.Carts.Where(p => p.CartId == cart_key_value);
            foreach (var item in carts)
            {
                count += item.Amount;
            }

            ViewData["CartCount"] = count;

            return PartialView("CartSummary");
        }
    }
}