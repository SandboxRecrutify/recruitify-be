using System;
using System.Threading.Tasks;

namespace Recrutify.Services.Events.Abstract
{
    public interface IUpdateStatusEventPublisher
    {
        public event Func<UpdateStatusEventArgs, Task> StatusCompleted;

        public event Action<AssignedInterviewEventArgs> AssignedInterview;

        public void OnStatusUpdated(UpdateStatusEventArgs e);

        public void OnAssignedInterview(AssignedInterviewEventArgs e);
    }
}
