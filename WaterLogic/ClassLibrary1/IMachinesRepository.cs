using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMachinesRepository
    {
        bool Create(Machine machine);
        bool Edit(Machine machine);
        bool Delete(int id);
        Machine Get(int id);
        IQueryable<Machine> GetAll();
    }
}
