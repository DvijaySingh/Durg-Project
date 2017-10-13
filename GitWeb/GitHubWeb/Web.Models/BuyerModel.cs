using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BuyerModel
    {
        public long BuyerID { get; set; }
        public long? CustomerCode { get; set; }
        [Required (ErrorMessage ="Buyer name is required")]
        public string BuyerName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
       // [Required(ErrorMessage = "Deposite Amount is required")]
        public  decimal? DepositeAmount { get; set; }
        public  decimal? OutstandingAmount { get; set; }
        [Required(ErrorMessage = "BuyDate is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime? BuyDate { get; set; }
        public byte[] Bill { get; set; }
        public  decimal? InterestRate { get; set; }
        public decimal? Interest { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime? finalDate { get; set; }
        public int? PageNo { get; set; }
        public  decimal? InterstableAmount { get; set; }
    }
}
