
using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class ApplicationUser:Entity
    {
      
        /// <summary>
        /// Unique bir değer olmalıdır. Oturum açan kullanıcı adı
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Unique bir değer olmalıdır. Oturum açan kullanıcı E-posta adresi
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Kullanıcı şifreleri Hash algoritması ile şifrelenir. Şifrelenmiş password değeri.
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Kullanıcının gerçek ismi. Profil oluşturmak için zorunlu değildir. 
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Kullanıcının soyismi. Profil oluşturmak için zorunlu değildir.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Varsa kullanıcının 2.ismi. Profil oluşturmak için zorunlu değildir.
        /// </summary>
        public string MiddleName { get; private set; }
        /// <summary>
        /// Kullanıcının Profil Fotoğrafı Uri. Profil oluşturmak için zorunlu değildir.
        /// </summary>
        public string ProfilePictureUrl { get; private set; }

        /// <summary>
        /// Minimumda bir kullanıcı username ve email adresini belirdik. Email zorunlu bir alandır.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// 

        public bool EmailVerified { get; private set; }


        public ApplicationUser(string email)
        {
            Id = Guid.NewGuid().ToString();
            SetEmail(email);
            SetUserName(email);
        }

        /// <summary>
        /// // eğer username boş geçilirse otomatik olarak username email olarak sistem tarafından set edilecektir.
        /// </summary>
        /// <param name="username"></param>
        public void SetUserName(string username)
        {
            
            if (string.IsNullOrEmpty(username))
            {
                this.UserName = this.Email;
            }

            this.UserName = username.Trim();
        }

        /// <summary>
        /// Kullanıcının Email bilgisini değiştemek için kullanabiliriz.
        /// </summary>
        /// <param name="email"></param>
        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("E-posta boş geçilemez");
            }

            this.Email = email.Trim();
        }

        /// <summary>
        /// Kullanıcıya hasklenmiş bir parola tanımlamak için kullanırız.
        /// </summary>
        /// <param name="passwordHash"></param>
        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new Exception("Parola alanı boş geçilemez");
            }

            this.PasswordHash = passwordHash.Trim();
        }

        /// <summary>
        /// Kullanıcıya ait profil özellliklerini değiştirmek için kullanırız.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="middleName"></param>
        public void SetProfileInfo(string firstName,string lastName, string middleName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new Exception("FirstName boş geçilemez");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new Exception("LastName boş geçilemez");
            }

            this.FirstName = firstName.Trim();
            this.LastName = lastName.Trim().ToUpper();
            this.MiddleName = middleName.Trim();
        }

        /// <summary>
        /// Kullanıcının profil fotoğrafını değiştemek için kullanırız.
        /// </summary>
        /// <param name="profilePictureUrl"></param>
        public void SetProfilePicture(string profilePictureUrl)
        {
            if (string.IsNullOrEmpty(profilePictureUrl))
            {
                throw new Exception("Profil Fotoğrafı boş geçilemez");
            }

            this.ProfilePictureUrl = profilePictureUrl.Trim();
        }

        public void SetVerifyEmail()
        {
            EmailVerified = true;
        }


    }
}
