using System;
using System.Collections.Generic;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Events
{
    public class UpdateStatusEventArgs : EventArgs
    {
        public IEnumerable<Guid> CandidatesIds { get; set; }

        public StatusDTO CandidateStatus { get; set; }

        public Guid ProjectId { get; set; }
    }
}
