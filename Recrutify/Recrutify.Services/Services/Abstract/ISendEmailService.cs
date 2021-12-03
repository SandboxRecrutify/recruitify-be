using System.Threading.Tasks;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}
