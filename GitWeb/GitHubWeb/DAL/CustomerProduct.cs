//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerProduct
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> OrderID { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> AppxWeight { get; set; }
        public Nullable<decimal> ActualWeight { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> Updatedby { get; set; }
    }
}
