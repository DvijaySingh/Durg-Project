﻿namespace Web.Models
{
    public class SellerProductModel
    {
        public long SellerProductID { get; set; }
        public string Type { get; set; }
        public  long? SellerID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? Unit { get; set; }
        public decimal? weight { get; set; }
        public  long? CategoryID { get; set; }
    }
}
