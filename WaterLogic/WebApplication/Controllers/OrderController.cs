using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.OrderReference;
using WebApplication.MachinesReference;
using WebApplication.Models;
using System.ServiceModel;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {

        private IOrderService proxy = new OrderServiceClient("orderHttpEndpoint");
        private IMachinesService proxyMachine = new MachinesServiceClient("machinesHttpEndpoint");
        public ActionResult ShoppingCart(string error)
        {
            if (User.Identity.IsAuthenticated)
            {
                IList<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                var shoppingcart = proxy.GetShoppingCartAsync(User.Identity.GetUserId()).Result;
                foreach (var item in shoppingcart)
                {
                    MachineOfferModel machine = new MachineOfferModel() { Id = item.MachineInCart.Id, Name = item.MachineInCart.Name, Price = item.MachineInCart.Price };
                    ShoppingCartItem cartItem = new ShoppingCartItem() { Quantity = item.Quantity, MmachineItem = machine, Id = item.Id };
                    shoppingCartItems.Add(cartItem);
                }
                if (error == null)
                    error = "";

                    ShopingCartOrderModel model = new ShopingCartOrderModel() { ShoppingCartItems = shoppingCartItems, Error = error };
               
                return View("ShoppingCart", model);
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult AddToCart(int machineId, int quantity)
        {
            var machine = proxyMachine.GetAsync((int)machineId).Result;
            int type = 3;

            switch (machine.Type)
            {
                case Model.MachineType.Small:
                    type = 1;
                    break;
                case Model.MachineType.Medium:
                    type = 2;
                    break;
            }
            if (User.Identity.GetUserId() == null)
            {
                return View("Details",  new AddMachineModel() { Id = machine.Id, Name = machine.Name, Description = machine.Description, Price = machine.Price, Quantity = machine.Quantity, Type = type, Succed = false });
            }
            var result=  proxy.AddToCartAsync(new Model.ShoppingCartItemForAdding() { CustomerId = User.Identity.GetUserId(), MachineInCartId =(int) machineId, Quantity = quantity });
           
            if (result.Result)
            {
               
                return View("Details", new AddMachineModel() { Id = machine.Id, Name = machine.Name, Description = machine.Description, Price = machine.Price, Quantity = machine.Quantity, Type = type, Succed= true });

            }
            else
            {
                return View("Details",  new AddMachineModel() { Id = machine.Id, Name = machine.Name, Description = machine.Description, Price = machine.Price, Quantity = machine.Quantity, Type = type, Succed = false });

            }
        }
        public ActionResult RemoveFromCart(int id)
        {
            proxy.RemoveFromCart(id);
            return RedirectToAction("ShoppingCart");

        }
        public ActionResult CreateOrder()
        {
            bool result = false;
            try
            {
                result = proxy.CreateOrder(User.Identity.GetUserId());
            }
            catch(FaultException e)
            {

                return RedirectToAction("ShoppingCart","Order", new {error = "Machines not in stock." });

            }
            if (result)
            {
                return RedirectToAction("MyOrders");
            }
            else
            {
                return RedirectToAction("ShoppingCart");
            }

        }

        public ActionResult MyOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                IList<MyOrderModel> list = new List<MyOrderModel>();
                var customerOrders = proxy.GetAllForCustomerAsync(User.Identity.GetUserId()).Result;
                foreach (var order in customerOrders)
                {
                    IList<SalelineModel> salelinesForOrder = new List<SalelineModel>();
                    foreach (var saleline in order.Salelines)
                    {
                        var machine = proxyMachine.GetAsync(saleline.MachineId).Result;
                        salelinesForOrder.Add(new SalelineModel() { MAchineId = saleline.MachineId, Price = machine.Price * saleline.Quantity, MachineName = machine.Name, Quantity = saleline.Quantity });

                    }
                    list.Add(new MyOrderModel() { Id = order.Id, Payed = order.Payed, TotalPrice = order.TotalPrice, Salelines = salelinesForOrder });
                }
                return View("MyOrders", list);
            }
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Pay(int id)
        {
            proxy.PayForOrder(id);
            return RedirectToAction("MyOrders");

        }
        public ActionResult DeleteOrder(int id)
        {
            proxy.RemoveOrder(id);
            return RedirectToAction("MyOrders");

        }
    }
}