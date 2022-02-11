using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class Project:Entity
    {
        public string Name { get; private set; }
        public string Description { get; set; }

        private List<Contributor> contributors = new List<Contributor>();
        public IReadOnlyList<Contributor> Contributers => contributors;

        public string CreatedBy { get; private set; }



        public Project(string name, string description, string createdBy)
        {
            Id = Guid.NewGuid().ToString();
            SetName(name);
            Description = description;
            CreatedBy = createdBy; 
        }

        /// <summary>
        /// Projeye isim verildikten sonra bir daha proje ismi değişemez.
        /// </summary>
        /// <param name="name"></param>
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Proje ismi giriniz");
            }

            Name = $"{name.Trim()}-{DateTime.Now.ToString("d")}";

        }


        /// <summary>
        /// Projeye contributor ekleme işlemi olsun
        /// </summary>
        /// <param name="contributor"></param>
        public void AddContributor(Contributor contributor)
        {
            // Projeye dahil etme isteğinde bulunduk
            // eğer kullanıcı mail adresinden isteği kabul et butonuna basarsa bu durumda projede contributor olarak görünebilecek ve projeye erişm izni olacak.

            if(contributors.Any(x=> x.UserId == contributor.UserId))
            {
                throw new Exception("Aynı user aynı projeye contritor olarak eklenemez");
            }
            else
            {
                // aynı contributor eklenemez
                // contibuter eklenirken contributor state waitingforrequest olarak ayalarnır.
                contributors.Add(contributor);
                DomainEvent.Raise(new ContributorSendAccessRequestEvent(this.Name, this.Id, contributor.UserId));
            }

            
        }

        /// <summary>
        /// Yanlışlıkla eklenen bir kullanıcıyı projeden geri aldık
        /// </summary>
        /// <param name="contributor"></param>
        public void RemoveContributor(Contributor contributor)
        {
            contributors.Remove(contributor);
            DomainEvent.Raise(new ContributorRevokeAccessEvent(this.Name,contributor.UserId));
        }

    }
}
