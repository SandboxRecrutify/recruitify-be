using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Options;
using Recrutify.DataAccess.Configuration;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendQueueEmailService : ISendQueueEmailService
    {
        private readonly IFormEmailService _formEmailService;
        private readonly ISendEmailService _sendEmailService;

        private readonly MailSettings _mailSettings;

        public SendQueueEmailService(IFormEmailService formEmailService, ISendEmailService sendEmailService, IOptions<MailSettings> mailSettings)
        {
            _formEmailService = formEmailService;
            _sendEmailService = sendEmailService;
            _mailSettings = mailSettings.Value;
        }

        public void SendEmailAsync()
        {
            var requests = _formEmailService.GetEmailRequestsAsync();
            var sender = _formEmailService.GetEmailSenderAsync();
            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmail(sender, emailRequest));
            }
        }
    }
}
