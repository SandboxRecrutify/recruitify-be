using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
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

        public async Task SendEmail(List<CandidateDTO> candidates, StatusDTO status, ProjectDTO project)
        {
            IEnumerable<EmailRequest> requests;
            switch (status)
            {
                case StatusDTO.Accepted:
                    requests = _formEmailService.FormAcceptanceEmail(candidates, project);
                    break;
                case StatusDTO.Declined:
                    requests = _formEmailService.FormDeclinedEmail(candidates, project);
                    break;
                default:
                    requests = _formEmailService.FormWaitingListEmail(candidates, project);
                    break;
            }

            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(emailRequest));
            }
        }
    }
}
