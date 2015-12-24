using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EDISAngular.Services
{
    public class EmailUtilities
    {
        public static void SendEmailToUser(string userId, string content, string title, string from)
        {
            throw new NotImplementedException();
        }
    }

    public class DirectToFileMailService : IIdentityMessageService
    {

        public System.Threading.Tasks.Task SendAsync(IdentityMessage message) {
            using (var smtpClient = new SmtpClient()) {
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                //smtpClient.PickupDirectoryLocation = @"c:\Emails";


                var fromAddress = new MailAddress("adviser1@ediservices.com.au", "EDIS Management Team");

                const string fromPassword = "bXmV9a?N";

                var smtp = new SmtpClient {
                    //Host = "smtp.gmail.com",
                    //Port = 587,
                    Host = "smtpout.asia.secureserver.net",
                    Port = 80,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (MailMessage email = new MailMessage(fromAddress, new MailAddress(message.Destination)) {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                }) {
                    smtp.Send(email);
                };
                return Task.FromResult(0);

                //using (var message2 = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //})
                //{
                //    smtp.Send(message2);
                //}
                //return Task.FromResult(0);

                //email.From = new MailAddress("address@mail.com", "Test");

                //email.To.Add(new MailAddress(message.Destination));

                //smtpClient.Send(email);
                //return Task.FromResult(0);
            }
        }
    }
}