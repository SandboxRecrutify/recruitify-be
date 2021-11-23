namespace Recrutify.Services.Events.Abstract
{
    public interface IUpdateStatusEventArgs
    {
        event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public void OnStatusUpdated(UpdateStatusEventArgs e);
    }
}
