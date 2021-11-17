using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MimeKit;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        MimeMessage CreateEmailAsync(EmailSender sender, EmailRequest emailRequest);

        IEnumerable<EmailRequest> GetEmailRequestsAsync();

        EmailSender GetEmailSenderAsync();
    }
}
