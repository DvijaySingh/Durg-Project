using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BulkBuyModel
    {
        public long BulkBuyID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public  decimal? TakenAmount { get; set; }
        public decimal? Rate { get; set; }
        public string Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? CloseDate { get; set; }
        public  decimal? ClosingAmount { get; set; }
    }
}
