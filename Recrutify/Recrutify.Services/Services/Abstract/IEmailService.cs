using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}
