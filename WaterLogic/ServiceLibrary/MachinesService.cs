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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MachinesService" in both code and config file together.
    public class MachinesService : IMachinesService
    {
        IMachinesRepository repository = new MachinesRepository();
        public bool Create(Machine machine)
        {
            if (machine.Price > 0 && machine.Quantity > 0)
            {
               return repository.Create(new Repository.DbConnection.Machine()
                {
                    Name = machine.Name,
                    Description = machine.Description,
                    Price = (float)machine.Price,
                    Quantity = machine.Quantity,
                    MachineType = new Repository.DbConnection.MachineType()
                    {
                        Type = machine.Type.ToString(),
                    }

                });
            }
            return false;
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        

        public bool Edit(Machine machine)
        {
            if (machine.Price > 0 && machine.Quantity > 0)
            {
                return repository.Edit(new Repository.DbConnection.Machine()
                {
                    Id=machine.Id,
                    Name = machine.Name,
                    Description = machine.Description,
                    Price = (float)machine.Price,
                    Quantity = machine.Quantity,
                    MachineType = new Repository.DbConnection.MachineType()
                    {
                        Type = machine.Type.ToString(),
                    },

                });
            }
            return false;
        }

        public Machine Get(int id)
        {
            var machineFromDb = repository.Get(id);
            return new Machine() { Name = machineFromDb.Name, Description = machineFromDb.Description, Price = machineFromDb.Price, Quantity = machineFromDb.Quantity, Id = machineFromDb.Id, Type = (MachineType)Enum.Parse(typeof(MachineType), machineFromDb.MachineType.Type.ToString()) };
        }

        public IList<Machine> GetAll()
        {
            IList<Machine> machines = new List<Machine>();
            var fromDb = repository.GetAll();
            foreach (var machineFromDb in fromDb)
            {
                machines.Add(new Machine() { Name = machineFromDb.Name, Description = machineFromDb.Description, Price = machineFromDb.Price, Quantity = machineFromDb.Quantity, Id = machineFromDb.Id, Type = (MachineType)Enum.Parse(typeof(MachineType), machineFromDb.MachineType.Type.ToString()) });
            }
            return machines;
        }

        public IList<Machine> SearchByName(string name)
        {
            IList<Machine> machines = new List<Machine>();
            var fromDb = repository.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower()));
            foreach (var machineFromDb in fromDb)
            {
                machines.Add(new Machine() { Name = machineFromDb.Name, Description = machineFromDb.Description, Price = machineFromDb.Price, Quantity = machineFromDb.Quantity, Id = machineFromDb.Id, Type = (MachineType)Enum.Parse(typeof(MachineType), machineFromDb.MachineType.Type.ToString()) });
            }
            return machines;
        }
    }
}
