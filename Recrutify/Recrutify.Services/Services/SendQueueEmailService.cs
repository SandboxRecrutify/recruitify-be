using System.Collections.Generic;
using Hangfire;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class SendQueueEmailService : ISendQueueEmailService
    {
        private readonly IFormWaitingLisrEmailService _formWaitingLisrEmailService;
        private readonly IFormDeclinationEmailService _formDeclinationEmailService;
        private readonly IFormAcceptanceEmailService _formAcceptanceEmailService;
        private readonly ISendEmailService _sendEmailService;

        public SendQueueEmailService(IFormDeclinationEmailService formDeclinationEmailService, ISendEmailService sendEmailService, IFormWaitingLisrEmailService formWaitingLisrEmailService, IFormAcceptanceEmailService formAcceptanceEmailService)
        {
            _formDeclinationEmailService = formDeclinationEmailService;
            _sendEmailService = sendEmailService;
            _formWaitingLisrEmailService = formWaitingLisrEmailService;
            _formAcceptanceEmailService = formAcceptanceEmailService;
        }

        public void SendEmail(List<CandidateDTO> candidates, Status status)
        {
            var requests = status == Status.Declined ? _formDeclinationEmailService.GetEmailRequests(candidates)
                : status == Status.WaitingList ? _formWaitingLisrEmailService.GetEmailRequests(candidates)
                : _formAcceptanceEmailService.GetEmailRequests(candidates);
            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmail(emailRequest));
            }
        }
    }
}
