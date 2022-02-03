using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class UserCreatedEvent : IDomainEvent
    {
        public string Name { get; set; } = "UserAccountCreated";
        public ApplicationUser Args { get; private set; }

        // event içerisinde taşınacak olan değer. user bilgisi taşınacak
        /// <summary>
        /// Taşınacak olan bu user bilgisine Args (Argümanlar) deriz.
        /// </summary>
        /// <param name="applicationUser"></param>
        public UserCreatedEvent(ApplicationUser applicationUser)
        {
            Args = applicationUser;
        }
    }
}
