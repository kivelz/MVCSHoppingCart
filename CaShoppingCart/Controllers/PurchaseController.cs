using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaShoppingCart.Models;
using CaShoppingCart.Models.Extended;

namespace CaShoppingCart.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            int cid = new int();
            if (Session["UserId"] != null)
            {
                cid = (int)Session["UserId"];

            }

            IList<PurchaseVM> view = new List<PurchaseVM>();

            using (CATEAM13BEntities db = new CATEAM13BEntities())
            {
                
                IList<Purchase> pp = db.Purchases.Where(x => x.CustomerId == cid).ToList();
                foreach (var p in pp)
                {
                    Product pro = db.Products.Where(x => x.ProductId == p.ProductId).FirstOrDefault();
                    bool flag = false;
                    foreach (var vv in view)
                    {
                        if (vv.ProductId == p.ProductId && vv.PurchaseDate == p.PurchaseDate && flag == false)
                        {
                            vv.Quantity = vv.Quantity + 1;
                            vv.ActivationCode.Add(p.ActivationCode);
                            flag = true;
                        }
                    }
                    if (flag == false)
                    {
                        PurchaseVM v = new PurchaseVM
                        {
                            ProductId = p.ProductId,
                            ProductImage = pro.ProductImage,
                            ProductName = pro.ProductName,
                            PurchaseDate = p.PurchaseDate,
                            Quantity = 1,
                            DownloadLink = pro.DownloadLink
                        };
                        v.ActivationCode.Add(p.ActivationCode);

                        view.Add(v);
                    }
                }
            }
                return View(view);
        }
    }
}