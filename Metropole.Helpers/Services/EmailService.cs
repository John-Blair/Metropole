using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Metropole.Helpers
{
    public class GridClientEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await SendEmailAsync(message);
        }

       

        private async Task SendEmailAsync(IdentityMessage message)
        {
            var apiKey = EnvironmentSecret.Instance.EmailApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@sendgrid.com", "Metropole Admin");
            var subject = message.Subject + ":JB3";
            var to = new EmailAddress(message.Destination, message.Destination);
            var plainTextContent = message.Body ;
            var htmlContent = message.Body;
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //if (!string.IsNullOrEmpty(msg.ReplyTo.Email))
            //{
            //    msg.SetReplyTo(new EmailAddress("john_blair@hotmail.com", "John Blair"));
            //}
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine("Response StatusCode is:" + response.StatusCode + message.Destination);


        }
    }
}