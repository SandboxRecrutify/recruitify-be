using System;
using Recrutify.Services.Events.Abstract;

namespace Recrutify.Services.Events
{
    public delegate void SaveDetailsHandler(UpdateStatusEventArgs args);

    public class UpdateStatusEventProcessor : IUpdateStatusEventProcessor
    {
        public event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public void OnStatusUpdated(UpdateStatusEventArgs e)
        {
            UpdateStatusByIdsAsyncComlited?.Invoke(e);
        }
    }
}
