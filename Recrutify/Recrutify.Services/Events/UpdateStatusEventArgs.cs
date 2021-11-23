using System;
using System.Collections.Generic;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Events
{
    public class UpdateStatusEventArgs : EventArgs
    {
        public IEnumerable<Guid> Ids { get; set; }

        public StatusDTO Status { get; set; }

        public Guid ProjectId { get; set; }
    }
}
