using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mdp.Models;
using Microsoft.AspNet.Identity.Owin;

namespace mdp.Controllers
{
    public class StartsController : Controller
    {
        // GET: Starts
        public ActionResult Starts(ProductSearch obj)
        {
            var context = HttpContext.GetOwinContext().Get<ProductsModel>();

            if (obj.ProductCategory >= 0)
            {
                // perforn search by product category
                var prods_category =
                    context.Products.Where(p => p.CategoryId == (CategoryType)obj.ProductCategory).ToList();

                return View(prods_category);
            }
            else if (string.IsNullOrEmpty(obj.SearchText))
            {
                // perform search for newest products and provide to start page
                var prods_newest = (from p in context.Products
                             orderby p.DateAdded
                             select p).Take(10).ToList();

                return View(prods_newest);
            }

            // perforn search by text
            var prods_search = (from p in context.Products
                         orderby p.DateAdded
                         where p.ProductName.Contains(obj.SearchText) || p.Description.Contains(obj.SearchText)
                         select p).ToList();

            return View(prods_search);
        }

        public ActionResult ByePrompt(mdp.Models.Product product)
        {
            return View(product);
        }

        public ActionResult ProductDeatails(mdp.Models.Product product)
        {
            return View(product);
        }
    }
}