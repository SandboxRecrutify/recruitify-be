using System.Collections.Generic;
using Hangfire;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendQueueEmailService : ISendQueueEmailService
    {
        private readonly IFormEmailService _formEmailService;
        private readonly ISendEmailService _sendEmailService;

        public SendQueueEmailService(IFormEmailService formEmailService, ISendEmailService sendEmailService)
        {
            _formEmailService = formEmailService;
            _sendEmailService = sendEmailService;
        }

        public void SendEmail(List<Candidate> candidates)
        {
            var requests = _formEmailService.GetEmailRequests(candidates);
            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmail(emailRequest));
            }
        }
    }
}
