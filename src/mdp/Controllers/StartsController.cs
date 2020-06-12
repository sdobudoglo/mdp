using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    DBContext.Products.Include(a => a.Comments)
                                      .Include(c => c.Images)
                                      .Where(p => p.CategoryId == (CategoryType)obj.ProductCategory).ToList();

                return View(prods_category);
            }
            else if (string.IsNullOrEmpty(obj.SearchText))
            {
                // perform search for newest products and provide to start page

                var prods_newest = DBContext.Products.Include(a => a.Comments)
                                                     .Include(c => c.Images)
                                                     .OrderByDescending(p => p.DateAdded)
                                                     .Take(10).ToList();

                return View(prods_newest);
            }

            // perforn search by text
            var prods_search = DBContext.Products.Include(a => a.Comments)
                                                     .Include(c => c.Images)
                                                     .Where(p => p.ProductName.Contains(obj.SearchText) || p.Description.Contains(obj.SearchText))
                                                     .OrderByDescending(p => p.DateAdded).ToList();

            return View(prods_search);
        }

        public ActionResult ProductDeatails(int product_id)
        {
            var product = DBContext.Products.Include(a => a.Comments)
                                            .Include(c => c.Images)
                                            .SingleOrDefault(p => p.Id == product_id);

            return View(product);
        }
    }
}