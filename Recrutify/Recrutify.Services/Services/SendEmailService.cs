using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
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

        public void SendEmail(EmailSender sender, EmailRequest emailRequest)
        {
            var email = _formEmailService.CreateEmailAsync(sender, emailRequest);
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(sender.Email, sender.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}