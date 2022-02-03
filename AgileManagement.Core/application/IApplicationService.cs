using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    /// <summary>
    /// Uygulama gelen iş isteklerini yakalayı işlemek için açtığımız service (use-case işlemleri)
    /// </summary>
    /// <typeparam name="TRequest">Uygulama gelen iş isteği</typeparam>
    /// <typeparam name="TResponse">Uygulamadan çıkan sonuç yani son kullanıcıya dönecek olan result </typeparam>
   public interface IApplicationService<in TRequest, out TResponse>
    {
        TResponse OnProcess(TRequest @request);
    }
}
