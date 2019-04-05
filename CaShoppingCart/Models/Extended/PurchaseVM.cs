
using System.Linq;
using System.Web;

namespace CaShoppingCart.Models.Extended
{
    using System;
    using System.Collections.Generic;

    public class PurchaseVM
    {
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string DownloadLink { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public IList<System.Guid> ActivationCode = new List<System.Guid>();
    }
}