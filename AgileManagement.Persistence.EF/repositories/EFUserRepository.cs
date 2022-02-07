using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.repositories;
using AgileManagement.Persistence.EF.migrations.appuser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Persistence.EF
{
    public class EFUserRepository : EFBaseRepository<ApplicationUser, UserDbContext>, IUserRepository
    {
        public EFUserRepository(UserDbContext dbContext) : base(dbContext)
        {
        }

        public override void Add(ApplicationUser entity)
        {
            base.Add(entity);

        }


    }
}
