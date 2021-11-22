using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendQueueEmailService : ISendQueueEmailService
    {
        private readonly IFormEmailService _formEmailService;
        private readonly ISendEmailService _sendEmailService;

        public SendQueueEmailService(ISendEmailService sendEmailService, IFormEmailService formEmailService)
        {
            _formEmailService = formEmailService;
            _sendEmailService = sendEmailService;
        }

        public async Task SendEmail(List<CandidateDTO> candidates, StatusDTO status)
        {
            var requests = status == StatusDTO.Declined ? _formEmailService.FormDeclinedEmail(candidates)
                : status == StatusDTO.WaitingList ? _formEmailService.FormWaitingListEmail(candidates)
                : _formEmailService.FormAcceptanceEmail(candidates);

            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(emailRequest));
            }
        }
    }
}
