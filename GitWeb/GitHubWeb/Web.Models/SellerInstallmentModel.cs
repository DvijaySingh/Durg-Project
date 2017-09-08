using System;

namespace Web.Models
{
    public class SellerInstallmentModel
    {
        public long SellerInstallmentID { get; set; }
        public long? SellerID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
    }
}
