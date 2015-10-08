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

        public System.Threading.Tasks.Task SendAsync(IdentityMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpClient.PickupDirectoryLocation = @"c:\Emails";



                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.Host = "smtp.gmail.com";
                //smtpClient.Port = 587;
                //smtpClient.Credentials = new NetworkCredential("address@mail.com", "Test");
                //smtpClient.EnableSsl = true;


                MailMessage email = new MailMessage()
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };

                email.From = new MailAddress("address@mail.com", "Test");

                email.To.Add(new MailAddress(message.Destination));

                smtpClient.Send(email);
                return Task.FromResult(0);
            }
        }
    }
}