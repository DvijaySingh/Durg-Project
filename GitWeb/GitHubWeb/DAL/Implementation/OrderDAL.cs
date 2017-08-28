using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace DAL.Implementation
{
    public class OrderDAL : IOrder
    {
        public void AddOrder(OrderModel orderModel)
        {

        }

        public List<CustomerProductModel> GetCurrentOrderProducts()
        {
            List<CustomerProductModel> lstcsproducts = new List<CustomerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var lstproducts = db.CustomerProducts.Where(m => m.OrderID == 0).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        CustomerProductModel objcsproduct = new CustomerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }

                }
                catch (Exception)
                {

                }
                return lstcsproducts;
            }
        }
    }
}
