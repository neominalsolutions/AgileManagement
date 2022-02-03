using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Core
{
    public  interface IEmailService
    {
        /// <summary>
        /// Tek bir şahısa mail atacağız.
        /// C# da async kod yapısını çalıştırmak için Task ile işaretliyoruz.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendSingleEmailAsync(string to, string subject, string message);
    }
}
