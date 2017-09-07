using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    
                    customerProductModel.IsActive = true;
                    customerProductModel.OrderID=customerProductModel.OrderID == null ? 0 : customerProductModel.OrderID;
                    CustomerProduct customerProduct = null;
                    if (customerProductModel.ProductID > 0)
                    {
                        customerProduct = GetCustomerProduct(db, customerProductModel.ProductID);
                    }
                    else
                    {
                        customerProduct = new CustomerProduct();
                    }
                    customerProductModel.CopyProperties(customerProduct);
                    if (customerProductModel.ProductID == 0)
                    {
                        db.CustomerProducts.Add(customerProduct);
                    }
                    db.SaveChanges();
                    var lstproducts = db.CustomerProducts.Where(m => m.OrderID == customerProductModel.OrderID && m.IsActive).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        CustomerProductModel objcsproduct = new CustomerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }
                    return lstcsproducts;
                }
                catch (Exception )
                {
                    return lstcsproducts;
                }
            }
        }

        public List<CustomerProductModel> DeleteCustomerProduct(long Id, long orderId)
        {
            List<CustomerProductModel> lstcsproducts = new List<CustomerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    CustomerProduct customerProduct = GetCustomerProduct(db, Id);
                    customerProduct.IsActive = false;
                    db.CustomerProducts.Remove(customerProduct);
                    db.SaveChanges();
                    var lstproducts = db.CustomerProducts.Where(m => m.OrderID == orderId && m.IsActive).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        CustomerProductModel objcsproducts = new CustomerProductModel();
                        cusprod.CopyProperties(objcsproducts);
                        lstcsproducts.Add(objcsproducts);
                    }

                }
                catch (Exception )
                {

                }
                return lstcsproducts;
            }
        }

        public CustomerProduct GetCustomerProduct(ShopDevEntities db, long Id)
        {
            CustomerProduct objCustomerProduct = null;
            try
            {
                objCustomerProduct = db.CustomerProducts.Where(m => m.ProductID == Id).FirstOrDefault();
            }
            catch (Exception )
            {

            }
            return objCustomerProduct;

        }

        public List<CustomerProductModel> UpdateCustomerProduct(CustomerProductModel customerProductModel)
        {
            throw new NotImplementedException();
        }
    }
}
