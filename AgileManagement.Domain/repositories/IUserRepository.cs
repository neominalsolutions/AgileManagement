using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public  interface IUserRepository:IRepository<ApplicationUser>
    {
        ApplicationUser FindUserByEmail(string email);
    }
}
