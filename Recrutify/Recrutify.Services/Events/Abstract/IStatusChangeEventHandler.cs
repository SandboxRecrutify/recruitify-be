namespace Recrutify.Services.Events.Abstract
{
    public interface IStatusChangeEventHandler
    {
        event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public void OnStatusUpdated(UpdateStatusEventArgs e);
    }
}
