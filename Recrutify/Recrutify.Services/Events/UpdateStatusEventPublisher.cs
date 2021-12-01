using System;
using System.Threading.Tasks;
using Recrutify.Services.Events.Abstract;

namespace Recrutify.Services.Events
{
    public class UpdateStatusEventPublisher : IUpdateStatusEventPublisher
    {
        public event Func<UpdateStatusEventArgs, Task> StatusCompleted;

        public void OnStatusUpdated(UpdateStatusEventArgs e)
        {
             StatusCompleted?.Invoke(e);
        }
    }
}
