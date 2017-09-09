using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SellerInstallmentModel
    {
        public long SellerInstallmentID { get; set; }
        public long? SellerID { get; set; }
        public decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
    }
}
