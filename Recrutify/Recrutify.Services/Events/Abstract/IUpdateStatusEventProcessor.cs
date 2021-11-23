using System;

namespace Recrutify.Services.Events.Abstract
{
    public interface IUpdateStatusEventProcessor
    {
        event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public void OnStatusUpdated(UpdateStatusEventArgs e);
    }
}
