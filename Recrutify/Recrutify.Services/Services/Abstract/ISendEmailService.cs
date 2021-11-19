using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailService
    {
        void SendEmail(EmailRequest request);
    }
}
