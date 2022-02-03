using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Kimliği doğrulanmış kullanıcı bilgilerini döndürür.
    /// </summary>
    public class AuthenticatedUser
    {
        /// <summary>
        /// Eposta bilgisi
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Oturumu açan kullancı bilgisi
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Oturum için kullanılan Token Değeri
        /// </summary>
        public string AccessToken { get; private set; }
        
        /// <summary>
        /// Oturum Süresi dolduduğu takdirde bir daha login olması lazım
        /// </summary>
        public DateTime? AccountExpirationDate { get; private set; }

        public AuthenticatedUser()
        {

        }

        public void SetAuthenticatedUser(string email, string username, string accessToken, DateTime accountExpirationDate)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("E-posta boş geçilemez");

            if (!email.Contains("@"))
                throw new Exception("E-posta formatında giriş yapınız");

            if (string.IsNullOrEmpty(accessToken))
                throw new Exception("Access token gerekli!");

            // eğer yanlışlık ile şuandan küçük bir değer seçilmiş ise  minimumda 20 dakikalık oturum aç
            if (accountExpirationDate <= DateTime.Now)
                accountExpirationDate = DateTime.Now.AddMinutes(20);

            // username boş ise email olarak default da ayarla
            if (string.IsNullOrEmpty(username))
                username = email.Trim();

            Email = email.Trim();
            UserName = username.Trim();
            AccessToken = accessToken.Trim();
            AccountExpirationDate = accountExpirationDate;
           
        }


    }
}
