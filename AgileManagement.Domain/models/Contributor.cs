using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    /// <summary>
    /// Bu contributor projeye olan isteği beklemede mi mail adresinden onaylamadımı
    /// kabul ettimiş yoksa ret ettimi bunları kontrol edeceğiz.
    /// </summary>
    public enum ContributorStatus
    {

        WaitingForRequest = 100,
        Accepted = 101, // Accepted olanlar Projede task alabilecekler.
        Rejected = 102


    }

    public class Contributor:Entity
    {
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public int Status { get; private set; }

        /// <summary>
        /// UserId ile hangi user hesabına bağlı olduğu bilgisini tutacağız. Buradan contribute ile alakalı email,firstname gibi değerlere erişebiliriz.
        /// </summary>
        /// <param name="userId"></param>
        public Contributor(string userId)
        {
            Id = Guid.NewGuid().ToString();

            if(string.IsNullOrEmpty(userId))
            {
                throw new Exception("UserId girilmedi!");
            }

    
           
            UserId = userId; // projeye eklenen bir contributor default da waitingForRequesttir.
            this.Status = (int)ContributorStatus.WaitingForRequest;
        }

        /// <summary>
        /// Contribute'a mail gittiğinde bu mail üzerinden rejected yada accepted yapabileceğiş düşünülerek açıldı
        /// </summary>
        /// <param name="contributorStatus"></param>
        public void ChangeProjectAccess(ContributorStatus contributorStatus)
        {
            // bir daha contributor status waitingForRequest'e çekilemez.
            if(ContributorStatus.WaitingForRequest != contributorStatus)
            {
                this.Status = (int)contributorStatus;
            }
       
        }

        

    }
}
