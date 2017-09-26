using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BuyerBillModel
    {
        public long BillID { get; set; }
        public long? BuyerID { get; set; }
        public string BillName { get; set; }
        public DateTime? BillDate { get; set; }
        public byte[] Image { get; set; }
    }
}
