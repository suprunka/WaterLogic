using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
         bool AddToCart(Cart cart);
         bool RemoveFromCart(int cartid);
        bool CreateOrder(OrderTable o);
        double AddSalelineToOrder(Cart s, int orderId);
        bool PayForOrder(int orderIds);
        OrderTable GetOrder(int orderId);
        bool RemoveOrder(int id);
        IQueryable<OrderTable> GetAll();
        IQueryable<Cart> GetShoppingCart(string customerId);



    }
}
