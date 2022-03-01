using AgileManagement.Core;
using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Persistence.EF
{
    public class EFProductBacklogItemRepository : EFBaseRepository<ProductBackLogItem, AppDbContext>, IProductBackLogItemRepository
    {
        public EFProductBacklogItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
