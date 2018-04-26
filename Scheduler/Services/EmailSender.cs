using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private ILogger<EmailSender> _logger;
        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation("SendEmailAsync " + email + "\\\\ " + subject + "\\\\ " + message);

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Admin", "egle.testmail@gmail.com"));
            mimeMessage.To.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("[smtpserveraddress]", 465, true);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("[smtpusertname]", "[smtppassword]");

                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
