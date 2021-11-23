using System;
using System.Collections.Generic;
using Hangfire;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
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

        public void SendEmail(List<Candidate> candidates, StatusDTO status, Project project)
        {
            var requests = status switch
            {
                StatusDTO.Accepted => _formEmailService.GetEmailRequests(candidates, project, Constants.TemplatePath.AcceptanceTemplate),
                StatusDTO.Declined => _formEmailService.GetEmailRequests(candidates, project, Constants.TemplatePath.DeclinationTemplate),
                StatusDTO.WaitingList => _formEmailService.GetEmailRequests(candidates, project, Constants.TemplatePath.WaitingListTemplate),
                _ => throw new ArgumentException(string.Format($"Email is not sent when user is transfered in status {0}", status))
            };

            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmailAsync(emailRequest));
            }
        }
    }
}
