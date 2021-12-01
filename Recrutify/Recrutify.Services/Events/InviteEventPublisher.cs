using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Events
{
    public class InviteEventPublisher
    {
        public event Action<AssignedInterviewEventArgs> AssignedInterview;

        public void OnAssignedInterview(AssignedInterviewEventArgs e)
        {
            AssignedInterview?.Invoke(e);
        }
    }
}
