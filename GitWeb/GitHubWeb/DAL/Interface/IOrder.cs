using System.Collections.Generic;
using Web.Models;

namespace DAL.Interface
{
    public interface IOrder
    {
        List<OrderModel> GetAllOrders();
        void AddOrder(OrderModel orderModel);
        List<CustomerProductModel> GetCurrentOrderProducts();
        OrderModel GetOrder(long orderID);
    }
}
