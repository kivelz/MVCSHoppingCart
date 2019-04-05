using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaShoppingCart.Models;

namespace CaShoppingCart.Utiliti
{
    public static class UsernameVerification
    {
        public static bool IsUsernameExist(string username)
        {
            using (CATEAM13BEntities db = new CATEAM13BEntities())
        {
            var person = db.Customers.FirstOrDefault(a => a.Username == username);
            return person != null;
        }
        }
    }
}