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
    
    public partial class BulkBuy
    {
        public long BulkBuyID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> TakenAmount { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> GWeight { get; set; }
        public Nullable<decimal> SWeight { get; set; }
        public string Status { get; set; }
    }
}