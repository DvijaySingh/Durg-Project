using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BuyerModel
    {
        public long BuyerID { get; set; }
        public long? CustomerCode { get; set; }
        //[Required (ErrorMessage ="Buyer name is required")]
        public string BuyerName { get; set; }
       // [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
       // [Required(ErrorMessage = "Deposite Amount is required")]
        public  decimal? DepositeAmount { get; set; }
        public  decimal? OutstandingAmount { get; set; }
        //[Required(ErrorMessage = "BuyDate is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime? BuyDate { get; set; }
        public byte[] Bill { get; set; }
    }
}
