using System.Collections.Generic;
using System.Net.Mail;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> GetEmailMessage();
    }
}
