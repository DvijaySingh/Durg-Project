using System;

namespace Web.Models
{
    public  class BorrowerModel
    {
        public long BorrowID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public  decimal? Amont { get; set; }
        public   DateTime? Date { get; set; }
        public string Status { get; set; }
    }
}
