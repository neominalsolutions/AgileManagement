using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure.notification.smtp
{
    public class NetSmtpEmailService : IEmailService
    {
        public async Task SendSingleEmailAsync(string to, string subject, string message)
        {
            // yani smptp sunucu üzerinden mail atacağımızı SmtpClient ile berliliyoruz
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // dünyayda mail için smptp 25 protu kullanılır. fakat artık neredeyse tüm dünyada 25 portu spama düştüğünde daha güvenli bir port olan 587 portu tecih edilir.
                Credentials = new NetworkCredential("iea.agilemanagement@gmail.com", "Nbuysabah34@"),
                EnableSsl = true, // mail gönderilirken şifreleme uygulanmasın mı güvenlik için önemli
            };

            // NetworkCredential ile hangi mail hesabı üzerinden mail atacağız kısmı

            try
            {
                smtpClient.Send("iea.agilemanagement@gmail.com", to, subject, message);
                await Task.CompletedTask;
                // eğer bir Task olan methodlarda bir response döndürmez isek bunları await Task.CompletedTask olarak işaretleyip methodun başalı bir şekild bitmesini sağlamalıyız.
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
       
            }
        }
    }
}
