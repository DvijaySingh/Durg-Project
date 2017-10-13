using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModel
{
    public class BulkBuySearchViewModel
    {
        public BulkBuyModel BulkBuy { get; set; }
        public List<BulkBuyModel> AllBulks { get; set; }
    }
}
