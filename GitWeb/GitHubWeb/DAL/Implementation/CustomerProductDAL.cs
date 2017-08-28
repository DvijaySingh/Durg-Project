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
                    customerProductModel.OrderID = 0;
                    customerProductModel.IsActive = true;
                    CustomerProduct customerProduct = new CustomerProduct();
                    customerProductModel.CopyProperties(customerProduct);
                    db.CustomerProducts.Add(customerProduct);
                    db.SaveChanges();
                    var lstproducts = db.CustomerProducts.Where(m => m.OrderID == 0 && m.IsActive).ToList();
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
                catch (Exception ex)
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
            catch (Exception ex)
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
