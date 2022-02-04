using AgileManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Persistence.EF
{

    /// <summary>
    /// Katmanlı mimari ile çalışırken ilgili katmandan migration alma işlemleri vs gibi durumlarda UserDbContextFactory ile dbContext ayağa kaldırılır. Production ortamında gerek kalmaz. Microsoft.EntityFrameworkCore.Design katmanlı mimari de bu paketi indirelim ve aşağıdaki gibi DbContext class olduğu yere IDesignTimeDbContextFactory implemente olan bir FactoryContext yazalım 
    /// ilgili proje katmanını seçip migration uygularız.
    /// </summary>
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {

        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=AgileManagementDb;Trusted_Connection=true");

            return new UserDbContext(optionsBuilder.Options);
        }
    }

    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions):base(dbContextOptions)
        {
 
        }

        public DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // migration işleminde bu konfigürasyon uygulanacaktır.
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
