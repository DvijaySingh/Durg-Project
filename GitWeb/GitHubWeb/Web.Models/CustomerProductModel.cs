using System;

namespace Web.Models
{
    public class CustomerProductModel
    {
        public long ProductID { get; set; }
        public long Type { get; set; }
        //public string Type { get {
        //        return TypeID == 1 ? "Silver" : "Gold";
        //    } }
        public string ProductName { get; set; }
        public long? OrderID { get; set; }
        public  decimal? AppxWeight { get; set; }
        public  decimal? ActualWeight { get; set; }
        public decimal? Amount { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public  DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime?  UpdatedDate { get; set; }
        public long? Updatedby { get; set; }
    }
}
