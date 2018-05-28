using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Metropole.Helpers;

namespace Metropole.Tests
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public async Task SendAnEmail()
        {
            var emailService = new GridClientEmailService();
            var message = new IdentityMessage();
            message.Body = "Hello a unit test message";
            message.Subject = "My Message ffs reworked";
            message.Destination = "john.9391.blair@outlook.com";
            await emailService.SendAsync(message);
            Console.WriteLine("Message Sent");
        }
    }
}
