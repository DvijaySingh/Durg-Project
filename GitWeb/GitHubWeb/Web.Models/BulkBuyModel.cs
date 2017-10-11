using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BulkBuyModel
    {
        public long BulkBuyID { get; set; }
        public long? CustomerCode { get; set; }
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public  decimal? TakenAmount { get; set; }
        [Required(ErrorMessage = "InerestRate is required")]
        public decimal? InterestRate { get; set; }
        public string Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? CloseDate { get; set; }
        public  decimal? ClosingAmount { get; set; }
        public  decimal? Interest { get; set; }
      
    }
}
