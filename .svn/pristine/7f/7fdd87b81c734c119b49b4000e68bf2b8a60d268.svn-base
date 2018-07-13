using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnguyen_Resume.Models
{
    public class MessageService
    {
        public async static Task SendEmailAsync(string name, string phone, string subject, string message)
        {
            try
            {
                var body = "<p> Phone: {0}</p><p> Name is : ({1})</p> <p>Message:</p> <p>{2}</p>";
                //var _email = "tnguyen3591@gmail.com";
                var _email = ConfigurationManager.AppSettings["EmailUser"];
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _dispName = "Contact tnguyen396";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add("nndt126@gmail.com");
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = string.Format(body, phone, name, message);
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}