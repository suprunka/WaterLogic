using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMachinesService" in both code and config file together.
    [ServiceContract]
    public interface IMachinesService
    {
        [OperationContract]
        bool Create(Machine machine);
        [OperationContract]
        bool Edit(Machine machine);
        [OperationContract]
        bool Delete(int id);
        [OperationContract]
        Machine Get(int id);
        [OperationContract]
        IList<Machine> GetAll();
        [OperationContract]
        IList<Machine> SearchByName(string name);
    }
}
