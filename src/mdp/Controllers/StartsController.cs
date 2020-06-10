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
        public ProductsModel DBContext
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ProductsModel>();
            }
        }

        // GET: Starts
        public ActionResult Starts(ProductSearch obj)
        {
            if (obj.ProductCategory >= 0)
            {
                // perforn search by product category
                var prods_category =
                    DBContext.Products.Where(p => p.CategoryId == (CategoryType)obj.ProductCategory).ToList();

                return View(prods_category);
            }
            else if (string.IsNullOrEmpty(obj.SearchText))
            {
                // perform search for newest products and provide to start page
                var prods_newest = (from p in DBContext.Products
                             orderby p.DateAdded descending
                             select p).Take(10).ToList();

                return View(prods_newest);
            }

            // perforn search by text
            var prods_search = (from p in DBContext.Products
                         orderby p.DateAdded descending
                                where p.ProductName.Contains(obj.SearchText) || p.Description.Contains(obj.SearchText)
                         select p).ToList();

            return View(prods_search);
        }

        public ActionResult ProductDeatails(int product_id)
        {
            var procuct = DBContext.Products.SingleOrDefault(p => p.Id == product_id);
            return View(procuct);
        }
    }
}