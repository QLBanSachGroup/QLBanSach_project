using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLBS;

namespace DAL_QLBS
{
    public class LoginDAL
    {
        BookManagementDataContext qlbs = new BookManagementDataContext();

        public LoginDAL()
        {

        }


        public bool getUserNameAndPassword(string username, string password)
        {
            return qlbs.users.Where(u => u.user_name == username && u.password == password).Any()?true:false;
        }
    }
}
