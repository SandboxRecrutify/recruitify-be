using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;
using Recrutify.Services.Settings;

namespace Recrutify.Services.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly MailSettings _mailSettings;

        public SendEmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendEmail(EmailRequest emailRequest)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse("danik.prokopenkov01@gmail.com"));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailRequest.Body };
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}