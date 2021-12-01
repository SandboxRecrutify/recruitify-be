using System.Net.Mail;
using System.Threading.Tasks;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailService
    {
        Task SendEmailAsync(EmailRequest request);

        Task SendEmailToInviteAsync(EmailRequestForInvite request);
    }
}
