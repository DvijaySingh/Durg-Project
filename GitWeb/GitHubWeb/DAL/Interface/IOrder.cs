using System.Collections.Generic;
using Web.Models;

namespace DAL.Interface
{
    public interface IOrder
    {
        void AddOrder(OrderModel orderModel);
        List<CustomerProductModel> GetCurrentOrderProducts();
        OrderModel GetOrder(long orderID);
    }
}
