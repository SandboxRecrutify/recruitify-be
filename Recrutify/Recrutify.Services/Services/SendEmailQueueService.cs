using System;
using System.Collections.Generic;
using Hangfire;
using Recrutify.DataAccess;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendEmailQueueService : ISendEmailQueueService
    {
        private readonly IFormEmailService _formEmailService;
        private readonly ISendEmailService _sendEmailService;

        public SendEmailQueueService(ISendEmailService sendEmailService, IFormEmailService formEmailService)
        {
            _formEmailService = formEmailService;
            _sendEmailService = sendEmailService;
        }

        public void SendEmailQueueForStatusChange(IEnumerable<CandidateDTO> candidates, StatusDTO status, ProjectDTO project)
        {
            var requests = status switch
            {
                StatusDTO.Accepted => _formEmailService.GetEmailRequestsForStatusChange(candidates, project, Constants.TemplatePath.AcceptanceTemplate),
                StatusDTO.Declined => _formEmailService.GetEmailRequestsForStatusChange(candidates, project, Constants.TemplatePath.DeclinationTemplate),
                StatusDTO.WaitingList => _formEmailService.GetEmailRequestsForStatusChange(candidates, project, Constants.TemplatePath.WaitingListTemplate),
                _ => throw new ArgumentException($"Email is not sent when user is transferred in status {status}")
            };

            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(emailRequest));
            }
        }

        public void SendEmailQueueForTest(IEnumerable<CandidateDTO> candidates, ProjectDTO project, DateTime testDeadlineDate, string emailToContact)
        {
            var requests = _formEmailService.GetEmailRequestsForSendTest(candidates, project, testDeadlineDate, emailToContact);
            SendEmailInBackgroundJob(requests);
        }

        public void SendEmailQueueForInvite(IEnumerable<InterviewEmailInfo> interviews)
        {
            var requests = _formEmailService.GetEmailRequestsForInterviewInvite(interviews);
            SendEmailInBackgroundJob(requests);
        }

        private void SendEmailInBackgroundJob(IEnumerable<EmailRequest> emailRequests)
        {
            foreach (var e in emailRequests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(e));
            }
        }
    }
}
