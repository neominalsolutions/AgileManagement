using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class ContributerSendAccessRequestHandler : IDomainEventHandler<ContributorSendAccessRequestEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public ContributerSendAccessRequestHandler(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public void Handle(ContributorSendAccessRequestEvent @event)
        {
            var user = _userRepository.Find(@event.UserId);

            string activationLink = "https://localhost:5001/Project/AcceptRequest?userId=" + @event.UserId + "&projectId=" + @event.ProjectId;

            _emailService.SendSingleEmailAsync(user.Email, $"{@event.ProjectName} için erişm izni isteği",$"<p> Projeye erişimi Kabul etmek için <a href='{activationLink}&accepted=true'> Tıklaynız <a/> Reddetmek için ise <a href='{activationLink}&accepted=false'>Tıklayınız<a></p>");

        }
    }
}
