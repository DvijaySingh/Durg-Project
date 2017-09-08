using System.Collections.Generic;

namespace Web.Models.ViewModel
{
    public class SellerViewModel
    {
        public SellerModel sellerInfor { get; set; }
        public SellerProductModel productInfo { get; set; }
        public List<SellerProductModel> Lstproducts { get; set; }
        public SellerInstallmentModel Installments { get; set; }
        public List<SellerInstallmentModel> LstInstallments { get; set; }
    }
}
