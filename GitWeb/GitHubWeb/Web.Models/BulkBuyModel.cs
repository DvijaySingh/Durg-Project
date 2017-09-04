using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BulkBuyModel
    {
        public long BulkBuyID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public  decimal? TakenAmount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? GWeight { get; set; }
        public  decimal? SWeight { get; set; }
        public string Status { get; set; }
    }
}
