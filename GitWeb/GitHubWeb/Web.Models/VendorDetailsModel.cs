using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class VendorDetailsModel
    {
        public int BuyVendorID { get; set; }
        public long? BulkByID { get; set; }
        public  int? VendorCode { get; set; }
        public string VendorName { get; set; }
       
        public string Address { get; set; }
        public string MobileNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? EndDate { get; set; }
        public  decimal? TakenAmount { get; set; }
        public  decimal? ReturnAmount { get; set; }
        public  decimal? InterestRate { get; set; }
        public decimal? Interest  { get; set; }
 
    }
}
