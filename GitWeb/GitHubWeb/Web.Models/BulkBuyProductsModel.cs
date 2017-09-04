using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BulkBuyProductsModel
    {
        public long BulkBuyProductID { get; set; }
        public long? BulkBuyID { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public  decimal? Weight { get; set; }
    }
}
