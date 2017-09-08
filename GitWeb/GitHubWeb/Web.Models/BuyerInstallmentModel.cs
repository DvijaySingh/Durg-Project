using System;

namespace Web.Models
{
    public class BuyerInstallmentModel
    {
        public long BuyerInstallmentID { get; set; }
        public  long? BuyerID { get; set; }
        public  decimal? Amount { get; set; }
        public   DateTime? Date { get; set; }
        public  bool? IsActive { get; set; }
    }
}
