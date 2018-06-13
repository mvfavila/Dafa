using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DAFA.Infra.CrossCutting.Identity.Configuration
{
    public class EmailService : IIdentityMessageService, IDisposable
    {
        public Task SendAsync(IdentityMessage message)
        {
            return SendMailAsync(message);
        }

        /// <summary>
        /// Sends an e-mail message.
        /// </summary>
        /// <param name="message">See <see cref="IdentityMessage"/>.</param>
        /// <returns>E-mail task.</returns>
        private static Task SendMailAsync(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true")
            {
                var msg = new MailMessage
                {
                    From = new MailAddress("admin@dafa.com", "DAFA Administrator"),
                    Subject = message.Subject,
                    IsBodyHtml = true
                };
                msg.To.Add(new MailAddress(message.Destination));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null,
                    MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null,
                    MediaTypeNames.Text.Html));

                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAccount"],
                    ConfigurationManager.AppSettings["EmailPassword"]);

                using(var smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587)))
                {
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                }
            }

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
