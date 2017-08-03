using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
   public class OrderModel
    {
        public long OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public  decimal? BookingAmount { get; set; }
        public  DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsDelevered { get; set; }
        public List<CustomerProductModel> lstcusProduct { get; set; }
    }
}
