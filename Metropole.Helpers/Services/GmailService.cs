﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        public static void SendMail(string to = "john_blair@hotmail.com", string subject = "test", string body = "test", string unsubscribeUserId ="")
        {
            var message = CreateMailMessage(to, subject, body);
            if (unsubscribeUserId.Length>0)
            {
                message = AddUnsubscribe(message, unsubscribeUserId);
            }
            SendMail (message);

        }

        public static void SendMailNewWhatsAppMember(string userName, string phoneNumber)
        {
            SendMail(CreateMailMessage(EnvironmentSecret.Instance.EmailAdmin, $"Add {userName} with {phoneNumber} to Metropole Whats App Group", userName + ":" + phoneNumber));
        }

        public static MailMessage AddUnsubscribe(MailMessage message, string unsubscribeUserId)
        {
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var unsubscribeUrl = url.AbsoluteAction("Unsubscribe", "Account", new { userId = unsubscribeUserId });


            message.Headers.Add("List-Unsubscribe", 
                String.Format(CultureInfo.InvariantCulture, "<{0}>", unsubscribeUrl));

            message.Body += $"<br/><br/>To Unsubscribe from further emails please click on this <a href=\"{unsubscribeUrl}\">Unsubscribe</a> link.<br/>";

            return message;
        }



    }
}