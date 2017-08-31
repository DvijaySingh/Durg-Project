using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class OrderModel
    {
        public long OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public  decimal? BookingAmount { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public  DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsDelevered { get; set; }
        public decimal? GRate { get; set; }
        public decimal? SRate { get; set; }
        public  CustomerProductModel cusProduct { get; set; }
        public List<CustomerProductModel> lstcusProduct { get; set; }
    }
}
