using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IFormEmailService _formEmailService;

        public SendEmailService(IOptions<MailSettings> mailSettings, IFormEmailService formEmailService)
        {
            _mailSettings = mailSettings.Value;
            _formEmailService = formEmailService;
        }

        public void SendEmail(EmailRequest emailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse("danik.prokopenkov01@gmail.com"));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailRequest.Body };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}