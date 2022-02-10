using AgileManagement.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{

    public static class DomainModule 
    {
      
        public static void Load(IServiceCollection services)
        {
            services.AddScoped<IUserDomainService, UserDomainService>();
            // event handlerlar her çağrıldığında sistem tarafından yeni bir instance alınsın.
            services.AddTransient<IDomainEventHandler<UserCreatedEvent>, UserCreatedHandler>();
            services.AddTransient<IDomainEventHandler<UserCreatedEvent>, UserCreatedNotifyHandler>();
            services.AddTransient<IDomainEventHandler<ContributorSendAccessRequestEvent>, ContributerSendAccessRequestHandler>();
            services.AddTransient<IDomainEventHandler<ContributorRevokeAccessEvent>, ContributorRevokeAccessEventHandler>();
        }
    }
}
