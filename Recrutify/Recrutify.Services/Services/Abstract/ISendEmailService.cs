using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailService
    {
        void SendEmail(EmailSender sender, EmailRequest request);
    }
}
