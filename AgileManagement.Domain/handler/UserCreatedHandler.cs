using AgileManagement.Core;
using AgileManagement.Domain.conts;
using Microsoft.AspNetCore.DataProtection;
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
        //private readonly ILogService _logService;
        private readonly IEmailService _emailService;
        private readonly IDataProtector _dataProtector;

        // ILogService logService,
        public UserCreatedHandler( IEmailService emailService, IDataProtectionProvider dataProtectionProvider)
        {
            //_logService = logService;
            _emailService = emailService;
            _dataProtector = dataProtectionProvider.CreateProtector(UserTokenNames.EmailVerification);
            // _dataProtector servisi ile userId şifreleyip daha sonra çözeceğiz
            // şifreli halini mail adresine göndereceğiz. böylelikle verificationCode direk oluşturmuş olacağız.
        }

        /// <summary>
        /// User Created Eventi alıp. Mail atacak bu arkadaş. Ve bu işlemi loglayacak
        /// </summary>
        /// <param name="event"></param>
        public void Handle(UserCreatedEvent @event)
        {
            // userId alanının protect et ona bir key oluştur
            string verificationCode = _dataProtector.Protect(@event.Args.Id);
            // bu kod alanının geri çevrildiğinde database'de unique bir alan olması lazım.

            string registerUri = "https://localhost:5001/account/confirm?verificationCode=" + verificationCode;


            string htmlString = $"<p>Hesabınızı aktive etmek için aşağıdaki linkte tıklayınız<a href={registerUri}>Aktive Et<a/></p>";

            //_logService.Log("User Account Successfully Created", LogLevels.Information);

            _emailService.SendSingleEmailAsync(to: @event.Args.Email, subject: "Hesap Aktivasyonu", htmlString);

        }
    }
}
