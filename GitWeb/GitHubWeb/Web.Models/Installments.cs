using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Installments
    {
        public long InstallmentID { get; set; }
        public long? BulkBuyID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InstallmentDate { get; set; }
    }
}
