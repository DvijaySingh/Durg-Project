using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class VendorModel
    {
        public int BuyVendorID { get; set; }
        public long? BulkByID { get; set; }
        public string VendorName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? EndDate { get; set; }
        public  decimal? TakenAmount { get; set; }
        public  decimal? ReturnAmount { get; set; }
        public  decimal? Rate { get; set; }
    }
}
