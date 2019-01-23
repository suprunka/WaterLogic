using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Repository;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EmployeeLogin : IEmployeeLogin
    {
        private EmployeeLoginRepository repository = new EmployeeLoginRepository();
        public bool CheckData(string login, string password)
        {
            return repository.CheckData(login, password);
        }

      
    }
}
