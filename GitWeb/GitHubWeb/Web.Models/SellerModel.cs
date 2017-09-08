using System;

namespace Web.Models
{
    public class SellerModel
    {
        public long SellerID { get; set; }
        public string SellerName { get; set; }
        public string Address { get; set; }
        public  decimal? DepositeAmount { get; set; }
        public  decimal? OutstandingAmount { get; set; }
        public  DateTime? BuyDate { get; set; }
        public string Status { get; set; }
    }
}
