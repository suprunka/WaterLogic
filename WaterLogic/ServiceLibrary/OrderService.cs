using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using Repository;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderService" in both code and config file together.
    public class OrderService : IOrderService
    {
        private IOrderRepository repository = new OrderRepository();
        public bool AddToCart(ShoppingCartItemForAdding cart)
        {
            return repository.AddToCart(new Repository.DbConnection.Cart()
            {
                CustomerId = cart.CustomerId,
                MachineId = cart.MachineInCartId,
                Quantity = cart.Quantity,
            });
        }

        public bool CreateOrder(string customerId)
        {
            return repository.CreateOrder(new Repository.DbConnection.OrderTable()
            {
                CustomerId = customerId,
                Payed = false,
                TotalPrice = 0,

            });
        }

        public IList<Order> GetAll()
        {
            IList<Order> result = new List<Order>();
            var fromDb = repository.GetAll();
            foreach (Repository.DbConnection.OrderTable item in fromDb)
            {
                IList<Saleline> salelinesModel = new List<Saleline>();
                var orderSalelines = item.Salelines;
                foreach (var saleline in orderSalelines)
                {
                    salelinesModel.Add(new Saleline() { Id = saleline.Id, MachineId = saleline.MachineId, Quantity = saleline.Quantity });
                }

                result.Add(new Order() { Id = item.Id, CustomerId = item.CustomerId, Payed = (bool)item.Payed, TotalPrice = (double)item.TotalPrice, Salelines = salelinesModel });
            }
            return result;
        }

        public IList<Order> GetAllForCustomer(string customerId)
        {

            IList<Order> result = new List<Order>();
            var fromDb = repository.GetAll().Where(x => x.CustomerId == customerId);
            foreach (Repository.DbConnection.OrderTable item in fromDb)
            {
                IList<Saleline> salelinesModel = new List<Saleline>();
                var orderSalelines = item.Salelines;
                foreach (var saleline in orderSalelines)
                {
                    salelinesModel.Add(new Saleline() { Id = saleline.Id, MachineId = saleline.MachineId, Quantity = saleline.Quantity });
                }

                result.Add(new Order() { Id = item.Id, CustomerId = item.CustomerId, Payed = (bool)item.Payed, TotalPrice = (double)item.TotalPrice, Salelines = salelinesModel });
            }
            return result;
        }

        public Order GetOrder(int orderId)
        {
            var item = repository.GetOrder(orderId);
            IList<Saleline> salelinesModel = new List<Saleline>();
            var orderSalelines = item.Salelines;
            foreach (var saleline in orderSalelines)
            {
                salelinesModel.Add(new Saleline() { Id = saleline.Id, MachineId = saleline.MachineId, Quantity = saleline.Quantity });
            }

            return new Order() { Id = item.Id, CustomerId = item.CustomerId, Payed = (bool)item.Payed, TotalPrice = (double)item.TotalPrice, Salelines = salelinesModel };

        }

        public IList<ShoppingCartItem> GetShoppingCart(string customerId)
        {
            IList<ShoppingCartItem> result = new List<ShoppingCartItem>();
            var cartfromDb = repository.GetShoppingCart(customerId);
            foreach (var item in cartfromDb)
            {
                var machine = new Machine() {Id = item.Machine.Id, Quantity =item.Machine.Quantity, Description = item.Machine.Description, Name = item.Machine.Name, Price = item.Machine.Price, Type = (MachineType)Enum.Parse(typeof(MachineType), item.Machine.MachineType.Type)};
                result.Add(new ShoppingCartItem() { Id = item.Id, CustomerId = item.CustomerId, Quantity = (int)item.Quantity, MachineInCart = machine });
            }
            return result;
        }

        public bool PayForOrder(int orderIds)
        {
            return repository.PayForOrder(orderIds);
        }

        public bool RemoveFromCart(int cartId)
        {
            return repository.RemoveFromCart(cartId);
        }

        public bool RemoveOrder(int id)
        {
            return repository.RemoveOrder(id);
        }
    }
}
