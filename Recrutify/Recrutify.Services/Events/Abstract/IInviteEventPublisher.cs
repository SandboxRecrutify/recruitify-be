using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Events.Abstract
{
    public interface IInviteEventPublisher
    {
        event Action<AssignedInterviewEventArgs> InterviewAssigned;

        void OnAssignedInterview(AssignedInterviewEventArgs e);
    }
}
