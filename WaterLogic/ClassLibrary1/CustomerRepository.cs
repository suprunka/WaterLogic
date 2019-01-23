using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository :ICustomer
    {
        private WaterDatabaseDataContext context;
        public CustomerRepository()
        {
            context = new WaterDatabaseDataContext(global::Repository.Properties.Settings.Default.dmai0917_1067608ConnectionString);
            //customerTable = context.Customers;
        }
        public bool Delete(string id)
        {
            var customer = context.Customers.FirstOrDefault(x => x.AspId == id);
            var customerAspNet = context.AspNetUsers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                context.Customers.DeleteOnSubmit(customer);
                context.AspNetUsers.DeleteOnSubmit(customerAspNet);
                context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool Edit(Customer customer)
        {
            var found = context.Customers.FirstOrDefault(x => x.AspId == customer.AspId);
            if (found != null)
            {
                found.Name = customer.Name;
                found.Address = customer.Address;
                context.SubmitChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Customer> GetAll()
        {
            return context.Customers.AsQueryable<Customer>();
        }

        public Customer GetCustomer(string id)
        {
            return context.Customers.FirstOrDefault(x => x.AspId == id);
        }

        public bool SetProperties(string name, string adddress, string aspId)
        {
            context.Customers.InsertOnSubmit(new Customer
            {
                Name = name,
                Address = adddress,
                AspId = aspId
            });
            context.SubmitChanges();
            return true;
        }

      
    }
}
