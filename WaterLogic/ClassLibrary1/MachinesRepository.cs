using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DbConnection;

namespace Repository
{
   public  class MachinesRepository : IMachinesRepository
    {
        private WaterDatabaseDataContext context = new WaterDatabaseDataContext(global::Repository.Properties.Settings.Default.dmai0917_1067608ConnectionString);
        public bool Create(Machine machine)
        {
            context.Machines.InsertOnSubmit(new Machine()
            {
                Name = machine.Name,
                Description = machine.Description,
                Price = machine.Price,
                Quantity = machine.Quantity,
                TypeId = context.GetTable<MachineType>().FirstOrDefault(x => x.Type == machine.MachineType.Type).Id


            });
            context.SubmitChanges();
            return true;
                
        }

        public bool Delete(int id)
        {
            var machine = context.Machines.FirstOrDefault(x => x.Id == id);
            if (machine != null)
            {
                context.Machines.DeleteOnSubmit(machine);
                context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool Edit(Machine machine)
        {
            var found = context.Machines.FirstOrDefault(x => x.Id == machine.Id);
            if (found != null)
            {
                found.Name = machine.Name;
                found.Price = machine.Price;
                found.Quantity = machine.Quantity;
                found.TypeId = context.GetTable<MachineType>().FirstOrDefault(x => x.Type == machine.MachineType.Type).Id;
                found.Description = machine.Description;
                context.SubmitChanges();

                return true;
            }
            return false;
        }

        public Machine Get(int id)
        {
            return context.Machines.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Machine> GetAll()
        {
            return context.Machines.AsQueryable<Machine>();
        }
    }
}
