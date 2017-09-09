using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SellerModel
    {
        public long SellerID { get; set; }
        public string SellerName { get; set; }
        public string Address { get; set; }
        public  decimal? DepositeAmount { get; set; }
        public  decimal? OutstandingAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? BuyDate { get; set; }
        public string Status { get; set; }
    }
}
