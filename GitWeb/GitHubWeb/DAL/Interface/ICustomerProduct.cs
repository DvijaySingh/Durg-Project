﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace DAL.Interface
{
   public interface ICustomerProduct
    {
         List<CustomerProductModel> AddCustomerProduct(CustomerProductModel customerProductModel);
        List<CustomerProductModel> UpdateCustomerProduct(CustomerProductModel customerProductModel);
        List<CustomerProductModel> DeleteCustomerProduct(long Id, long orderId);
        CustomerProduct GetCustomerProduct(ShopDevEntities db,long Id);

    }
}
