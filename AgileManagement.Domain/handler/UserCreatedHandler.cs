using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    /// <summary>
    /// DomainEvent.Raise olduğunda event fırlayacak. UserCreatedHandler da fırlayan bu eventi yakalayacak. Event ile alakalı işlemleri yapacak.
    /// </summary>
    public class UserCreatedHandler : IDomainEventHandler<UserCreatedEvent>
    {
        private readonly ILogService _logService;
        private readonly IEmailService _emailService;

        public UserCreatedHandler(ILogService logService, IEmailService emailService)
        {
            _logService = logService;
            _emailService = emailService;
        }

        /// <summary>
        /// User Created Eventi alıp. Mail atacak bu arkadaş. Ve bu işlemi loglayacak
        /// </summary>
        /// <param name="event"></param>
        public void Handle(UserCreatedEvent @event)
        {
            string registerUri = "https://localhost:5001/account/confirm?verificationCode=" + Guid.NewGuid().ToString();

            string htmlString = $"<p>Hesabınızı aktive etmek için aşağıdaki linkte tıklayınız<a href={registerUri}>Aktive Et<a/></p>";

            _logService.Log("User Account Successfully Created", LogLevels.Information);

            _emailService.SendSingleEmailAsync(to: @event.Args.Email, subject: "Hesap Aktivasyonu", htmlString);

        }
    }
}
