using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Events
{
    public class InviteEventProcessor
    {
        private readonly ISendEmailQueueService _sendQueueEmailService;
        private readonly IInviteEventPublisher _inviteEventPublisher;

        public InviteEventProcessor(ISendEmailQueueService sendQueueEmailService, IInviteEventPublisher inviteEventPublisher)
        {
            _sendQueueEmailService = sendQueueEmailService;
            _inviteEventPublisher = inviteEventPublisher;
        }

        public void Subscribe()
        {
            _inviteEventPublisher.InterviewAssigned += MailingInviteAsync;
        }

        public void MailingInviteAsync(AssignedInterviewEventArgs e)
        {
            _sendQueueEmailService.SendEmailQueueForInvite(e.Interviews);
        }
    }
}
