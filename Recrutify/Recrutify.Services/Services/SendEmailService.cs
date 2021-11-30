using System;
using System.Threading.Tasks;
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

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailRequest.Body };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, Convert.ToInt32(_mailSettings.Port), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}