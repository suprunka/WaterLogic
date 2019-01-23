using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using Repository;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in both code and config file together.
    public class CustomerService : ICustomerService
    {
        private CustomerRepository repository = new CustomerRepository();
        public bool Delete(string id)
        {
            return repository.Delete(id);
        }

        public bool Edit(Customer customer)
        {
            return repository.Edit(new Repository.DbConnection.Customer() { Name = customer.Name, Address = customer.Address, AspId = customer.AspId });
        }

        public Customer GetCustomer(string id)
        {
            var found = repository.GetCustomer(id);
            if (found != null)
            {
                return new Customer() { Name = found.Name, Address = found.Address, AspId = found.AspId };
            }
            return null;
        }
        public IList<Customer> SearchByName(string name)
        {
            IList<Customer> all = new List<Customer>();

            var customersFromDb = repository.GetAll().Where(x => x.Name == name);
            foreach (var customer in customersFromDb)
            {
                all.Add(new Customer() { Id = customer.Id, Name = customer.Name, Address = customer.Address, AspId = customer.AspId });
            }
            return all;
        }
        public bool SetProperties(Customer customer)
        {
            return repository.SetProperties(customer.Name, customer.Address, customer.AspId);
        }
        public IList<Customer> GetAll()
        {
            IList< Customer> all = new List<Customer>();
            var customersFromDb = repository.GetAll();
            foreach (var customer in customersFromDb)
            {
                all.Add(new Customer() {  Id = customer.Id, Name = customer.Name, Address = customer.Address, AspId = customer.AspId });
            }
            return all;
        }
    }
}
