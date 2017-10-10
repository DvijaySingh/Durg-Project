using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BorrowerInstallmentModel
    {
        public long InstallmentID { get; set; }
        public  long? BorrowerID { get; set; }
        public decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
    }
}
