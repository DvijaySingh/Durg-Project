using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CustomerProduct
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public long? OrderID { get; set; }
        public  decimal? AppxWeight { get; set; }
        public  decimal? ActualWeight { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public  DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime?  UpdatedDate { get; set; }
        public long? Updatedby { get; set; }
    }
}
