using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure
{
    public class NetSmtpEmailService : IEmailService
    {
        public async Task SendSingleEmailAsync(string to, string subject, string message)
        {
            // yani smptp sunucu üzerinden mail atacağımızı SmtpClient ile berliliyoruz
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // dünyayda mail için smptp 25 protu kullanılır. fakat artık neredeyse tüm dünyada 25 portu spama düştüğünde daha güvenli bir port olan 587 portu tecih edilir.
                Credentials = new NetworkCredential("ieanbuy.34@gmail.com", "istanbul.34"),
                EnableSsl = true, // mail gönderilirken şifreleme uygulanmasın mı güvenlik için önemli
               
            };

            // NetworkCredential ile hangi mail hesabı üzerinden mail atacağız kısmı

            try
            {
                var mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress("ieanbuy.34@gmail.com");
                mailMessage.Body = message;


                smtpClient.Send(mailMessage);
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
