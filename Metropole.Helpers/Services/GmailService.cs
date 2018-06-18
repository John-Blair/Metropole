using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Metropole.Helpers
{
    public class GMailService    {

        public static MailMessage CreateMailMessage(string to = "john_blair@hotmail.com", string subject = "test", string body = "test")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EnvironmentSecret.Instance.EmailAdmin);
            mail.ReplyToList.Add( new MailAddress(EnvironmentSecret.Instance.EmailAdmin));
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body =body;
            mail.IsBodyHtml = true;
            return mail;
        }

        public static void SendMail(MailMessage Message)
        {
            SmtpClient client = new SmtpClient();
            client.Host = EnvironmentSecret.Instance.SmtpHost;
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(
                EnvironmentSecret.Instance.NetworkCredentialUserName,
                EnvironmentSecret.Instance.NetworkCredentialPassword);
            client.Send(Message);
        }

        public static void SendMail(string to = "john_blair@hotmail.com", string subject = "test", string body = "test")
        {
            SendMail (CreateMailMessage(to, subject, body));

        }
    }
}