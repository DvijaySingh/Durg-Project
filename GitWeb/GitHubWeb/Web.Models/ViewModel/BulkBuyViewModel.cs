using System.Collections.Generic;

namespace Web.Models.ViewModel
{
    public class BulkBuyViewModel
    {
        public BulkBuyModel bulkBuyModel { get; set; }
        public VendorModel Vendors { get; set; }
        public Installments installments { get; set; }
        public List<VendorModel> lstVendors { get; set; }
        public BulkBuyProductsModel Products { get; set; }
        public List<BulkBuyProductsModel> lstbulkBuyProducts { get; set; }
        public List<Installments> lstinstallments { get; set; }
    }
}
