using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Configuration;
using CaShoppingCart.Models;
using CaShoppingCart.Utiliti;

namespace CaShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        // GET: LoginAndRegistration
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            //init the list
            if(ModelState.IsValid)
            {
                var isExist = UsernameVerification.IsUsernameExist(customer.Username);
                //if username exist
                if(isExist)
                {
                    ModelState.AddModelError("Username", "Username already exist");
                    return View(customer);
                }

                customer.Password = Crypto.Hash(customer.Password);
                using (CATEAM13BEntities dc = new CATEAM13BEntities())
                {
                    dc.Customers.Add(customer);
                    //save changes
                    dc.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
            }

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(Customer customer)
        {
            using (CATEAM13BEntities db = new CATEAM13BEntities())
            {

                customer.Password = Crypto.Hash(customer.Password);
                var userDetails = db.Customers.Where(x => x.Username == customer.Username && x.Password == customer.Password)
                    .FirstOrDefault();    
               
                if (userDetails == null)
                {
                    customer.LoginErrorMessage = "Username or password is incorrect";
                    return View("Login");
                }
                else
                {
                    Session["UserId"] = userDetails.CustomerId;
                    return RedirectToAction("index", "Product");
                }
            }
            
        }

        public ActionResult LogOut()
        {
            int UserId = (int) Session["UserId"];
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult HelloPartial()
        {
            if (Session["UserId"] != null)
            {
                int cid = (int)Session["UserId"];
                using (CATEAM13BEntities dd = new CATEAM13BEntities())
                {
                    Customer cc = dd.Customers.Where(x => x.CustomerId == cid).FirstOrDefault();
                    string hello = "Welcome " + cc.FirstName + " " + cc.LastName;
                    ViewData["hello"] = hello;
                }
            }
            return PartialView();
        }

    }
    
}