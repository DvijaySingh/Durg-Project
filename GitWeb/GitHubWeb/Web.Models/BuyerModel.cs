using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BuyerModel
    {
        public long BuyerID { get; set; }
        public string BuyerName { get; set; }
        public string Address { get; set; }
        public  decimal? DepositeAmount { get; set; }
        public  decimal? OutstandingAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime? BuyDate { get; set; }
    }
}
