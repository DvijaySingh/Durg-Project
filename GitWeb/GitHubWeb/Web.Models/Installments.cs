using System;

namespace Web.Models
{
    public class Installments
    {
        public long InstallmentID { get; set; }
        public long? BulkBuyID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InstallmentDate { get; set; }
    }
}
