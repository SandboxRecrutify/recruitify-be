using System;
using System.IO;
using System.Text;
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
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, Convert.ToInt32(_mailSettings.Port), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendEmailToInviteAsync(EmailRequestForInvite emailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;

            var emailBody = new BodyBuilder();
            emailBody.HtmlBody = emailRequest.Body;

            var ics = CreateInviteFile(emailRequest.DateTimeInterview); //учесnm UTC

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(ics.ToString());

                    writer.Flush();
                    stream.Position = 0;

                    emailBody.Attachments.Add("invite.ics", stream);
                }
            }

            email.Body = emailBody.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, Convert.ToInt32(_mailSettings.Port), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        private StringBuilder CreateInviteFile(DateTime dateTimeInterview)
        {
            StringBuilder str = new StringBuilder();  ////константы вынести 
            str.AppendLine("BEGIN:VCALENDAR");

            str.AppendLine("PRODID: Exadel.com");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("TZOFFSETTO:+0300");
            str.AppendLine("TZOFFSETFROM:+0300");
            str.AppendLine("BEGIN:VEVENT");

            str.AppendLine(string.Format("DTSTART;TZID=Europe/Minsk:{0:yyyyMMddTHHmmssZ}", dateTimeInterview));
            str.AppendLine(string.Format("DTEND;TZID=Europe/Minsk:{0:yyyyMMddTHHmmssZ}", dateTimeInterview.AddMinutes(30)));
            str.AppendLine(string.Format("LOCATION: {0}", "Belarus, Minsk"));
            str.AppendLine(string.Format("ORGANIZER;CN=EXADEL:mailto:exadel.recruitify@gmail.com"));
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", "Exadel: Invitation for an interview"));
            str.AppendLine(string.Format("SUMMARY:{0}", "Interview"));
            
            str.AppendLine("STATUS:CONFIRMED");
            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:Accept");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:BUSY");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
           str.AppendLine("END:VCALENDAR");
            return str;
        }
    }
}