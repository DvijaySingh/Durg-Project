using System.Collections.Generic;

namespace Web.Models.ViewModel
{
    public class BuyerViewModel
    {
        public BuyerModel buyer { get; set; }
        public BuyerProductModel productInfo { get; set; }
        public List<BuyerProductModel> LstProducts { get; set; }
        public BuyerInstallmentModel Installments { get; set; }
        public List<BuyerInstallmentModel> LstInstallments { get; set; }
    }
}
