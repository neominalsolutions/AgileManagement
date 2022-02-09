using AgileManagement.Core;
using AgileManagement.Domain;
using System.Linq;


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

        public ApplicationUser FindUserByEmail(string email)
        {
            return _dbSet.FirstOrDefault(x => x.Email == email);
        }
    }
}
