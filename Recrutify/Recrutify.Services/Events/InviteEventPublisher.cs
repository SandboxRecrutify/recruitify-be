using System;
using Recrutify.Services.Events.Abstract;

namespace Recrutify.Services.Events
{
    public class InviteEventPublisher : IInviteEventPublisher
    {
        public event Action<AssignedInterviewEventArgs> InterviewAssigned;

        public void OnAssignedInterview(AssignedInterviewEventArgs e)
        {
            InterviewAssigned?.Invoke(e);
        }
    }
}
