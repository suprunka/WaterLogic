using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public MachineOfferModel MmachineItem { get; set; }
        public int Quantity { get; set; }
    }
    public class ShopingCartOrderModel
    {
       public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
       public string Error { get; set; }
    }

    public class MyOrderModel
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<SalelineModel> Salelines { get; set; }
        //0- payed
        public bool Payed { get; set; }

    }
    public class SalelineModel
    {
        public int MAchineId { get; set; }
        public string MachineName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}