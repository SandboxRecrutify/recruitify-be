using Recrutify.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Events
{
    public class InviteEventProcessor
    {
        private readonly ISendEmailQueueService _sendQueueEmailService;

        public InviteEventProcessor(ISendEmailQueueService sendQueueEmailService)
        {
            _sendQueueEmailService = sendQueueEmailService;
        }

        public void Subscribe()
        {
            _updateStatusEventPublisher.AssignedInterview += MailingInviteAsync;
        }

        public void MailingInviteAsync(AssignedInterviewEventArgs e)
        {
            _sendQueueEmailService.SendEmailQueueForInvite(e.Interviews);
        }
    }
}
