using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLBS;
using ENUM;

namespace DAL_QLBS
{
    public class EmployeeDAL
    {
        BookManagementDataContext qlbs = new BookManagementDataContext();
        public EmployeeDAL()
        {

        }

        public IQueryable getListEmployee()
        {
            var urs = from us in qlbs.users join r in qlbs.roles on us.code_role equals r.code where us.code_role == Role.USER.ToString() select new {us.full_name, us.address, us.email, us.gender, us.phone, us.date_of_birth, r.name};
            return urs;
        }

        public bool insertEmployee(user us)
        {
            try
            {
                us.code_role = Role.USER.ToString();
                us.create_date = DateTime.Now;
                
                qlbs.users.InsertOnSubmit(us);
                qlbs.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
