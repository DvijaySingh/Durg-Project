using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModel
{
   public class CustomerViewModel
    {
        public CustomerModel customer { get; set; }
        public List<OrderModel> orders { get; set; }
        public List<BorrowerViewModel> borrower { get; set; }
        public List<BuyerViewModel> buyers { get; set; }
        public decimal OrderOutstanding { get; set; }
        public decimal BorrowOutstanding { get; set; }
        public decimal BuyOutstanding { get; set; }
    }
}
