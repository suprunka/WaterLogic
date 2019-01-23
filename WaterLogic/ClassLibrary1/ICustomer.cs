using Repository.DbConnection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface ICustomer
    {
        bool SetProperties(string name, string adddress, string aspId);
        bool Edit(Customer customer );
        bool Delete(string id);
        Customer GetCustomer(string id);
        IQueryable<Customer> GetAll ();
    }
}
