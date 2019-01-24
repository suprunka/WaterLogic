using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrderService" in both code and config file together.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        bool AddToCart(ShoppingCartItemForAdding cart);
        [OperationContract]
        bool RemoveFromCart(int cartId);
        [OperationContract]
        bool CreateOrder(string customerId);
        [OperationContract]

        bool PayForOrder(int orderIds);
        [OperationContract]

        Order GetOrder(int orderId);
        [OperationContract]

        bool RemoveOrder(int id);
        [OperationContract]
        IList<Order> GetAll();
        [OperationContract]
        IList<Order> GetAllForCustomer(string customerId);
        [OperationContract]
        IList<ShoppingCartItem> GetShoppingCart(string customerId);
    }
}
