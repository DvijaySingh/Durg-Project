using System;

namespace Web.Models
{
    public class BuyerModel
    {
        public long BuyerID { get; set; }
        public string BuyerName { get; set; }
        public string Address { get; set; }
        public  decimal? DepositeAmount { get; set; }
        public  decimal? OutstandingAmount { get; set; }
        public   DateTime? BuyDate { get; set; }
    }
}
