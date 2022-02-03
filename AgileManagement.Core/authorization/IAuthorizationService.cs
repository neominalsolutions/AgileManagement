using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Hesabı verilen kullanıcı sistemde yetkin mi değil mi kontrolü yapan servisimiz. Örneğin kullanıcının bir sayfayı görüntüleme yetkisi var mı yok mu işlemleri için veri tabanına bağlanıp kullanıcın yetkisi kontrol edilerek sayfanın görünüp görünmeyeceğine karar vereceğiz. Farklı bir sistem üzerinde kullanıcı hesapları tutulabileceğinden veya Google Facebook vs gibi farklı altyapılar üzerinden yetkilendirme sorgulanabileceğinden ötürü yine async olarak düşünülmüştür. Not Kullanıcı Authenticated olarak işaretlenmiş ise yani oturum açık ise bu servisden yararlanabilecektir.
    /// </summary>
    public interface IAuthorizationService
    {
        Task<bool> IsAuthorized(string email);
    }
}
