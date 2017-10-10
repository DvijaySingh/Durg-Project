using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModel
{
    public class BorrowerSearchViewModel
    {
        public BorrowerModel Borrower { get; set; }
        public List<BorrowerModel> AllBorrowers { get; set; }
    }
}
