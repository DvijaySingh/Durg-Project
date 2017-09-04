using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModel
{
    public class BulkBuyViewModel
    {
        public BulkBuyModel bulkBuyModel { get; set; }
        public List<VendorModel> lstVendors { get; set; }
        public List<BulkBuyProductsModel> lstbulkBuyProducts { get; set; }
    }
}
