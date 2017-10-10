using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public  class BorrowerModel
    {
        public long BorrowID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public  decimal? Amont { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime? Date { get; set; }
        public string Status { get; set; }
        public  long? CustCode { get; set; }
        public  decimal? InterestRate { get; set; }
        public  decimal? Interest { get; set; }
    }
}
