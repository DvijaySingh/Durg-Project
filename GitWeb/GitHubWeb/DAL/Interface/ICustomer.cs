using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace DAL.Interface
{
    public interface ICustomer
    {
        List<CustomerModel> AllCustomers(CustomerModel objModel);
        
    }
}
