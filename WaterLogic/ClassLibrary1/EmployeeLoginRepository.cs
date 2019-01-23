using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class EmployeeLoginRepository : IEmployeeLoginRepository
    {
        private WaterDatabaseDataContext context = new WaterDatabaseDataContext(global::Repository.Properties.Settings.Default.dmai0917_1067608ConnectionString);

        public bool CheckData(string login, string password)
        {
            var employee = context.EmployeeRegisters.FirstOrDefault(x => x.Name.Equals(login));
            if (employee != null)
            {
                if (employee.Password.Equals(password))
                    return true;
            }
            return false;
        }
    }
}
