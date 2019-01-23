using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public enum MachineType
    {
        Small, 
        Medium,
        Big
    }

    [KnownType(typeof(MachineType))]
    [DataContract]
    public class Machine
    {
      [DataMember]
        public int Id { get; set; }
        [DataMember]
         public string  Name { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public int  Quantity { get; set; }
        [DataMember]
        public double  Price { get; set; }
        [DataMember]
        public MachineType  Type { get; set; }
    }
}
