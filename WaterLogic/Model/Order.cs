using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]

        public double TotalPrice { get; set; }
        [DataMember]

        public IEnumerable<Saleline> Salelines { get; set; }
        [DataMember]

        //0- payed
        public bool Payed { get; set; }
    }

    public class Saleline
    {
        [DataMember]

        public int Id { get; set; }
        [DataMember]

        public int MachineId { get; set; }
        [DataMember]

        public int Quantity { get; set; }

    }
    public class ShoppingCartItem
    {
        [DataMember]

        public int Id { get; set; }
        [DataMember]

        public string CustomerId { get; set; }
        [DataMember]

        public Machine MachineInCart { get; set; }
        [DataMember]

        public int Quantity { get; set; }
    }
    public class ShoppingCartItemForAdding
    {
        [DataMember]

        public string CustomerId { get; set; }
        [DataMember]

        public int MachineInCartId { get; set; }
        [DataMember]

        public int Quantity { get; set; }
    }
    [Serializable]

    public class QuantityException : Exception
    {
        private ShoppingCartItem item;
        public QuantityException(ShoppingCartItem sitem)
        {
            item = sitem;
        }

        public ShoppingCartItem Item { get { return item; } set { item = value; } }
        public String GetMessage()
        {
            return item.MachineInCart.Name + " is " + item.MachineInCart.Quantity + " left. Your order can't be proccessed.";
        }
    }
}
