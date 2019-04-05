using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaShoppingCart.Models;

namespace CaShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        public ActionResult Index(string search)
        {
           List<Product> list = new List<Product>();

           using (CATEAM13BEntities db = new CATEAM13BEntities())
           {
                if(Session["UserId"] != null)
                {
                int cid = (int) Session["UserId"];
                Customer u = db.Customers.Where(x => x.CustomerId == cid).FirstOrDefault();
                ViewData["DisplayName"] = u.Username;
                }
                if(search == null)
                {
                    list = db.Products.ToList();
                }
                else
                {
                    list = db.Products.Where(a => a.ProductName.Contains(search) || a.ProductDesc.Contains(search)).ToList();
                }
           }

           ViewData["Products"] = list;
           return View();
               
        }
    }
}