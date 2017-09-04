using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
   public class VendorModel
    {
        public int BuyVendorID { get; set; }
        public long? BulkByID { get; set; }
        public string VendorName { get; set; }
        public  DateTime? StartDate { get; set; }
        public  DateTime? EndDate { get; set; }
        public  decimal? TakenAmount { get; set; }
        public  decimal? ReturnAmount { get; set; }
        public  decimal? Rate { get; set; }
    }
}
