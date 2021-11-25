using System;
using System.Collections.Generic;
using Hangfire;
using Recrutify.DataAccess;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendQueueEmailService : ISendEmailQueueService
    {
        private readonly IFormEmailService _formEmailService;
        private readonly ISendEmailService _sendEmailService;

        public SendQueueEmailService(ISendEmailService sendEmailService, IFormEmailService formEmailService)
        {
            _formEmailService = formEmailService;
            _sendEmailService = sendEmailService;
        }

        public void SendEmailQueueForStatusChange(IEnumerable<CandidateDTO> candidates, StatusDTO status, ProjectDTO project)
        {
            var requests = status switch
            {
                StatusDTO.Accepted => _formEmailService.GetEmailRequestsForChangeStatus(candidates, project, Constants.TemplatePath.AcceptanceTemplate),
                StatusDTO.Declined => _formEmailService.GetEmailRequestsForChangeStatus(candidates, project, Constants.TemplatePath.DeclinationTemplate),
                StatusDTO.WaitingList => _formEmailService.GetEmailRequestsForChangeStatus(candidates, project, Constants.TemplatePath.WaitingListTemplate),
                _ => throw new ArgumentException($"Email is not sent when user is transferred in status {status}")
            };

            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(emailRequest));
            }
        }
    }
}
