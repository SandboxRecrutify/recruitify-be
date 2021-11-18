﻿using Hangfire;
using Microsoft.Extensions.Options;
using Recrutify.Services.Configuration;
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

        public void SendEmail()
        {
            var requests = _formEmailService.GetEmailMessage();
            foreach (var emailRequest in requests)
            {
                BackgroundJob.Enqueue(() => _sendEmailService.SendEmail(emailRequest));
            }
        }
    }
}
