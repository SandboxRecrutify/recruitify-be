using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;
using MimeKit;
using Mustache;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormEmailService : IFormEmailService
    {
        private readonly MailSettings _mailSettings;

        public FormEmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public MimeMessage CreateEmailAsync(EmailSender sender, EmailRequest emailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(sender.Email);

            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailRequest.Body };
            return email;
        }

        public IEnumerable<EmailRequest> GetEmailRequestsAsync()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Message_Templates\\Test_Email.html";
            StreamReader str = new StreamReader(filePath);
            string mailText = str.ReadToEnd();
            str.Close();
            var emailRequests = new List<EmailRequest>();
            HtmlFormatCompiler compiler = new HtmlFormatCompiler();
            Generator generator = compiler.Compile(mailText);
            string email = generator.Render(new
            {
                name = "Даниил",
            });
            for (int i = 1; i < 10; i++)
            {
                emailRequests.Add(new EmailRequest()
                {
                    ToEmail = "danik.prokopenkov01@gmail.com",
                    Subject = "test" + i.ToString(),
                    Body = email,
                });
            }

            return emailRequests;
        }

        public EmailSender GetEmailSenderAsync()
        {
            var sender = new EmailSender()
            {
                Name = _mailSettings.DisplayName,
                Surname = _mailSettings.DisplaySurname,
                Email = _mailSettings.Mail,
                Password = _mailSettings.Password,
            };
            return sender;
        }
    }
}
