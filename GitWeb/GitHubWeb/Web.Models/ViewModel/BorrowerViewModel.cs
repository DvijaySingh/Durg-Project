using System.Collections.Generic;

namespace Web.Models.ViewModel
{
    public class BorrowerViewModel
    {
        public BorrowerModel Borrower { get; set; }
        public BorrowerInstallmentModel Installments { get; set; }
        public List<BorrowerInstallmentModel> Lstinstallments { get; set; }

    }
}
