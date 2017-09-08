using System;

namespace Web.Models
{
    public class BorrowerInstallmentModel
    {
        public long InstallmentID { get; set; }
        public  long? BorrowerID { get; set; }
        public decimal? Amount { get; set; }
        public   DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
    }
}
