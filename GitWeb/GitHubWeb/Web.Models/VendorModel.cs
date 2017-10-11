using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class VendorModel
    {
        public int VendorCode { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public  int? TotalBulks { get; set; }
    }
}
