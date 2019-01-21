using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Model;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        bool SetProperties(Customer customer);
        [OperationContract]
        bool Edit(Customer customer);
        [OperationContract]
        bool Delete(string id);
        [OperationContract]
        Customer GetCustomer(string id);
    }
}
