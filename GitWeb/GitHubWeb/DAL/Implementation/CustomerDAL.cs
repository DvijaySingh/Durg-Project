using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace DAL.Implementation
{
    public class CustomerDAL : ICustomer
    {
        public List<CustomerModel> AllCustomers(CustomerModel objModel)
        {
            List<CustomerModel> lstcustomers = new List<CustomerModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var res = from customer in db.Customers
                              where (customer.CustmerName.Contains(objModel.CustmerName) ||
                              customer.CustCode == objModel.CustCode)
                                || (string.IsNullOrEmpty(objModel.CustmerName) && objModel.CustCode == 0)

                              select customer;
                    foreach (var seller in res)
                    {
                        CustomerModel sellerModel = new CustomerModel();
                        seller.CopyProperties(sellerModel);
                        lstcustomers.Add(sellerModel);
                    }
                }
                catch
                {

                }
                return lstcustomers;
            }
        }
    }
}
