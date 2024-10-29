using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLBS;
using DTO_QLBS;

namespace BLL_QLBS
{
    public class EmployeeBLL
    {
        EmployeeDAL employee = new EmployeeDAL();
        public EmployeeBLL()
        {

        }

        public IQueryable getListEmployee()
        {
            return employee.getListEmployee();
        }
    }
}
