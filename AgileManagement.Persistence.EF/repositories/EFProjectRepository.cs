using AgileManagement.Core;
using AgileManagement.Domain;
using AgileManagement.Domain.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Persistence.EF.repositories
{
    public class EFProjectRepository : EFBaseRepository<Project, AppDbContext>, IProjectRepository
    {
        public EFProjectRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
