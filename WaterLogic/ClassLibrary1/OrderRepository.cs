using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

using System.Text;
using System.Threading.Tasks;
using Repository.DbConnection;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private WaterDatabaseDataContext context = new WaterDatabaseDataContext(global::Repository.Properties.Settings.Default.dmai0917_1067608ConnectionString);
        public double AddSalelineToOrder(Cart cartItem, int orderId)
        {
            int machineQuantityInDb = cartItem.Machine.Quantity;
            int orderedQuantity = (int) cartItem.Quantity;
            var x = cartItem.Machine.Quantity - cartItem.Quantity;
            if (x>=0)
            {
                cartItem.Machine.Quantity = cartItem.Machine.Quantity - (int)cartItem.Quantity;
                context.Salelines.InsertOnSubmit(new Saleline()
                {
                    MachineId = cartItem.MachineId,
                    Quantity = orderedQuantity,
                    OrderId = context.OrderTables.FirstOrDefault(t => t.Id == orderId).Id,

                });
                context.SubmitChanges();
                return orderedQuantity * cartItem.Machine.Price;
            }
            else
            {
                throw new Model.QuantityException( new Model.ShoppingCartItem() {
                    MachineInCart= new Model.Machine() { Name= cartItem.Machine.Name, Quantity= cartItem.Machine.Quantity, Id = cartItem.MachineId, Description= cartItem.Machine.Description, Price= cartItem.Machine.Price, Type = (Model.MachineType) Enum.Parse(typeof(Model.MachineType), cartItem.Machine.MachineType.Type)}
                } );
            }


        }

        public bool AddToCart(Cart cart)
        {
            context.Carts.InsertOnSubmit(cart);
            context.SubmitChanges();
            return true;
        }

        public bool CreateOrder(OrderTable o)
        {

            var txOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.Serializable
            };

            using (var myTran = new TransactionScope(TransactionScopeOption.Required, txOptions))
            {
                try
                {
                    context.OrderTables.InsertOnSubmit(o);
                    context.SubmitChanges();

                    var itemsFromCart = context.Carts.Where(x => x.CustomerId == o.CustomerId);

                    var order = context.OrderTables.FirstOrDefault(x => x.Id == o.Id);
                    double orderTotalPrice = 0;
                    foreach (var item in itemsFromCart)
                    {
                        orderTotalPrice =+ AddSalelineToOrder(item, o.Id);
                    }
                    order.TotalPrice = (float)orderTotalPrice;
                    context.Carts.DeleteAllOnSubmit(context.Carts.Where(x => x.CustomerId == o.CustomerId));
                    context.SubmitChanges();
                    myTran.Complete();
                    return true;
                }
                catch (Model.QuantityException e)
                {
                    throw e;

                }


            }
                
        }

        public IQueryable<OrderTable> GetAll()
        {
            return context.OrderTables.AsQueryable< OrderTable>();
        }

        public OrderTable GetOrder(int orderId)
        {
            return context.OrderTables.FirstOrDefault(x=>x.Id == orderId);
        }

        public IQueryable<Cart> GetShoppingCart(string customerId)
        {
            return context.Carts.Where(x => x.CustomerId == customerId);
           
        }

        public bool PayForOrder(int orderId)
        {
           var order =  context.OrderTables.FirstOrDefault(x => x.Id == orderId);
            order.Payed = true;
            context.SubmitChanges();
            return true;

        }

        public bool RemoveFromCart(int cartId)
        {
            var cart = context.Carts.FirstOrDefault(x => x.Id == cartId);
            if (cart != null)
            {
                context.Carts.DeleteOnSubmit(cart);
                context.SubmitChanges();
                return true;
            }
            return false;
           
        }

        public bool RemoveOrder(int id)
        {
            var txOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.RepeatableRead
            };

            using (var myTran = new TransactionScope(TransactionScopeOption.Required, txOptions))
            {
                var order = context.OrderTables.FirstOrDefault(x => x.Id == id);
                if (!(bool)order.Payed)
                {
                    var allOrderSalelines = context.Salelines.Where(x => x.OrderId == id);
                    foreach (var saleline in allOrderSalelines)
                    {
                        var x = saleline.Machine.Quantity + saleline.Quantity;
                        context.Salelines.DeleteOnSubmit(saleline);

                    }

                    context.OrderTables.DeleteOnSubmit(order);
                    context.SubmitChanges();
                    myTran.Complete();
                    return true;

                }
                return false;
            }
        }
    }
}
