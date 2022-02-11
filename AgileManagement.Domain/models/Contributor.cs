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
        public int Status { get; private set; }

        public Project Project { get; private set; }


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
          
            // şuanki current status Accepted rejected yapılamaz
            if ((int)ContributorStatus.WaitingForRequest == Status && contributorStatus != ContributorStatus.WaitingForRequest)
            {
                this.Status = (int)contributorStatus;
            }
        }

        /// <summary>
        /// Sadece yanlış bir project access işleminde süreci sıfırlamak için kullanırız.
        /// </summary>
        public void ResetProjectAccess()
        {
            // projeden yetkili kişi proje için yeniden istekte bulundu.
            this.Status = (int)ContributorStatus.WaitingForRequest;

            // Yeniden projeye erişim için mail attık
            DomainEvent.Raise(new ContributorSendAccessRequestEvent(this.Project.Name, this.Project.Id, this.UserId));
        }

    }
}
