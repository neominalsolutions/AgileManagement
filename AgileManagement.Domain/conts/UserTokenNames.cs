using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain.conts
{
    /// <summary>
    /// Kullanıcı için generate edilecek olan tokenlar için kullanacağız.
    /// </summary>
   public static  class UserTokenNames
    {
        public const string EmailVerification = "EmailVerificationToken";
        // 1.gün süre tanınabilir.
        public const string PhoneVerification = "PhoneVerificationToken";
    }
}
