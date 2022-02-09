using AgileManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Persistence.EF
{
    public static class EFModule
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
      

            services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("LocalDb"));
            });

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("LocalDb"));
            });

            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IProjectRepository, EFProjectRepository>();
            // best practice olarak db context uygyulaması appsettings dosyasından bilgileri conectionstrings node dan alırız.
        }
    }
}
