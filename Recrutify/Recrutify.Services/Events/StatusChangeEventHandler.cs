using Recrutify.Services.Events.Abstract;

namespace Recrutify.Services.Events
{
    public class StatusChangeEventHandler : IStatusChangeEventHandler
    {
        public event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public void OnStatusUpdated(UpdateStatusEventArgs e)
        {
            UpdateStatusByIdsAsyncComlited?.Invoke(e);
        }
    }
}