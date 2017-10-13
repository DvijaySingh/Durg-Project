using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Installments
    {
        public long InstallmentID { get; set; }
        public long? BulkBuyID { get; set; }
        public decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? InstallmentDate { get; set; }
        public string Description { get; set; }
    }
}
