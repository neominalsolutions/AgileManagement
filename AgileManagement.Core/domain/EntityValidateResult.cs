using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core.domain
{
    /// <summary>
    /// Entity ile ilgili servisler üzerinden işlem yapılarkan entity ait validasyondan geçip geçmediği tutacağız. Burada entity ile alakalı boş geçme gibi kurallar değil. Bussiness kurallara göre işlemleri yapacağız.
    /// </summary>
    public class EntityValidateResult
    {
        /// <summary>
        /// Entitynin kayıt altına alınmadan önce bir kontrolden geçi yada kaldığını gösterir
        /// </summary>
       public  bool IsValid { get; set; }

        /// <summary>
        /// Eğer isvalid değilse bu durumda entity ile ilgili hata mesajlarını tutarız
        /// </summary>
       public List<string> ErrorMessages { get; set; } = new List<string>();

    }
}
