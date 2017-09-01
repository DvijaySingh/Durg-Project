using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Web.Models;

namespace DAL.Implementation
{
    public class OrderDAL : IOrder
    {
        public List<OrderModel> GetAllOrders()
        {
            List<OrderModel> lstordes = new List<OrderModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var allorders = db.Orders.OrderByDescending(m => m.OrderId).ToList();
                    foreach (var cusprod in allorders)
                    {
                        OrderModel objcsproduct = new OrderModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstordes.Add(objcsproduct);
                    }

                }
                catch
                {

                }
                return lstordes;
            }
        }
        public void AddOrder(OrderModel orderModel)
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    Order order = null;
                    //if (orderModel.OrderId > 0)
                    //{
                    //    order = GetCustomerProduct(db, customerProductModel.ProductID);
                    //}

                    if (orderModel.OrderId == 0)
                    {
                        order = new Order();
                        orderModel.CopyProperties(order);
                        db.Orders.Add(order);
                    }
                    else
                    {
                        order = db.Orders.Where(m => m.OrderId == orderModel.OrderId).FirstOrDefault();
                        orderModel.CopyProperties(order);
                    }
                    db.SaveChanges();
                    List<CustomerProduct> lstNewproducts = db.CustomerProducts.Where(m => m.OrderID == 0).ToList();
                    lstNewproducts.ForEach(m => m.OrderID = order.OrderId);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
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

        public OrderModel GetOrder(long orderID)
        {
            OrderModel ordermodel = new OrderModel();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var order = db.Orders.Where(m => m.OrderId == orderID).FirstOrDefault();
                    var lstproducts = db.CustomerProducts.Where(m => m.OrderID == orderID).ToList();
                    List<CustomerProductModel> lstcsproducts = new List<CustomerProductModel>();
                    foreach (var cusprod in lstproducts)
                    {
                        CustomerProductModel objcsproduct = new CustomerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }
                    ordermodel.OrderId = order.OrderId;
                    ordermodel.CustomerName = order.CustomerName;
                    ordermodel.CustomerAddress = order.CustomerAddress;
                    ordermodel.BookingAmount = order.BookingAmount;

                    ordermodel.OrderDate = order.OrderDate;
                    ordermodel.DeliveryDate = order.DeliveryDate;
                    ordermodel.IsDelevered = order.IsDelevered;
                    ordermodel.GRate = order.GRate;
                    ordermodel.SRate = order.SRate;
                    ordermodel.OutstandingAmount = order.OutstandingAmount;
                    ordermodel.lstcusProduct = new List<CustomerProductModel>();
                    ordermodel.lstcusProduct.AddRange(lstcsproducts);

                }
                catch (Exception ex) { }
            }
            return ordermodel;
        }
    }
}
