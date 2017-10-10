using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BuyerInstallmentModel
    {
        public long BuyerInstallmentID { get; set; }
        public  long? BuyerID { get; set; }
        public  decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime? Date { get; set; }
        public  bool? IsActive { get; set; }
        public string Description { get; set; }
    }
}
