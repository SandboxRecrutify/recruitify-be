using System;
using System.Threading.Tasks;

namespace Recrutify.Services.Events.Abstract
{
    public interface IUpdateStatusEvent
    {
        event Func<UpdateStatusEventArgs, Task> StatusCompleted;

        public void OnStatusUpdated(UpdateStatusEventArgs e);
    }
}
