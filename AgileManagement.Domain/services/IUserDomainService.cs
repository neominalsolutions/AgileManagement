using AgileManagement.Core.domain;
using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class ApplicationUserResult
    {
        /// <summary>
        /// İşlem başarılı Mesajı
        /// </summary>
        public bool IsSucceeded { get; set; }

        /// <summary>
        /// User ile alakalı operasyonlar esnasında bir hata geldğinde buradaki Hata Mesajlarını son kullanıcıya göstereceğiz.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

    }

    public interface IUserDomainService: IDomainService<ApplicationUser>
    {
        

        /// <summary>
        /// Sisteme yeni bir user kaydı açmamızı sağlayan servis
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ApplicationUserResult CreateUser(string email, string password);

        /// <summary>
        /// Gönderiğimiz user'a göre profil bilgileri günceller
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="middlename"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ApplicationUserResult UpdateProfile(string firstname, string lastname, string middlename, ApplicationUser user);

        /// <summary>
        /// Gönderiğimiz user bilgisine göre profil fotosu günceller
        /// </summary>
        /// <param name="profilePictureUrl"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ApplicationUserResult UpdateProfilePicture(string profilePictureUrl, ApplicationUser user);
        
    }
}
