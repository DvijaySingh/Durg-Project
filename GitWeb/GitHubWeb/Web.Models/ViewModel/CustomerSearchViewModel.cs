using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModel
{
   public class CustomerSearchViewModel
    {
        public CustomerModel Customer { get; set; }
        public List<CustomerModel> Allcustomer { get; set; }
    }
}
