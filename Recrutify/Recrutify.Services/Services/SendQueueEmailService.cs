using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Recrutify.DataAccess;
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
                    requests = _formEmailService.FormEmail(candidates, project, Constants.TemplatePath.AcceptanceTemplate);
                    break;
                case StatusDTO.Declined:
                    requests = _formEmailService.FormEmail(candidates, project, Constants.TemplatePath.DeclinationTemplate);
                    break;
                default:
                    requests = _formEmailService.FormEmail(candidates, project, Constants.TemplatePath.WaitingListTemplate);
                    break;
            }

            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(emailRequest));
            }
        }
    }
}
