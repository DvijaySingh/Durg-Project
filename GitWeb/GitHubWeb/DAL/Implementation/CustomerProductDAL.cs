using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace DAL.Implementation
{
    public class CustomerProductDAL : ICustomerProduct
    {
        private ShopDevEntities db = new ShopDevEntities();
        
        public List<CustomerProductModel> AddCustomerProduct(CustomerProductModel customerProductModel)
        {
            List<CustomerProductModel> lstcsproducts = new List<CustomerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    customerProductModel.OrderID = 0;
                    CustomerProduct customerProduct = new CustomerProduct();
                    customerProductModel.CopyProperties(customerProduct);
                    db.CustomerProducts.Add(customerProduct);
                    db.SaveChanges();
                    var lstproducts = db.CustomerProducts.Where(m => m.IsActive == true).OrderByDescending(m => m.OrderID == 0).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        CustomerProductModel objcsproduct = new CustomerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }
                    return lstcsproducts;
                }
                catch (Exception ex)
                {
                    return lstcsproducts;
                }
            }
        }

        public List<CustomerProductModel> DeleteCustomerProduct(long Id)
        {
            throw new NotImplementedException();
        }

        public CustomerProductModel GetCustomerProduct(long Id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerProductModel> UpdateCustomerProduct(CustomerProductModel customerProductModel)
        {
            throw new NotImplementedException();
        }
    }
}
