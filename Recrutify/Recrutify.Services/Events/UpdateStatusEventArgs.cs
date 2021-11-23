using System;
using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.Events.Abstract;

namespace Recrutify.Services.Events
{
    public delegate void SaveDetailsHandler(UpdateStatusEventArgs args);

    public class UpdateStatusEventArgs : EventArgs, IUpdateStatusEventArgs
    {
        public event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public IEnumerable<Guid> Ids { get; set; }

        public StatusDTO Status { get; set; }

        public Guid ProjectId { get; set; }

        public void OnStatusUpdated(UpdateStatusEventArgs e)
        {
            UpdateStatusByIdsAsyncComlited?.Invoke(e);
        }
    }
}
