using System;
using System.Threading.Tasks;

namespace Recrutify.Services.Events.Abstract
{
    public interface IUpdateStatusEventPublisher
    {
        event Func<UpdateStatusEventArgs, Task> StatusCompleted;

        void OnStatusUpdated(UpdateStatusEventArgs e);
    }
}
