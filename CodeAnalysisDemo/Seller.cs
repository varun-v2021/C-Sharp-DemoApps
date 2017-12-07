using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysisDemo
{
    public class Seller: IGovt,ITax
    {
        public string SellerId { get; set; }
        public string SellerName { get; set; }
        public string[] SellerLocations { get; set; }

        public Seller(string sellerId, string sellerName, params string[] sellerLocations)
        {
            this.SellerId = sellerId;
            this.SellerName = sellerName;
            this.SellerLocations = sellerLocations;
        }

        //for tax dept
        double ITax.PayTax()
        {
            return 200;
        }

        //for govt
        double IGovt.PayTax()
        {
            return 100;
        }
    }
}
