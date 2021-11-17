using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class BulkUpdateStatusDTO
    {
        public StatusDTO Status { get; set; }

        public string Reason { get; set; }

        public IEnumerable<Guid> CandidatesIds { get; set; }

        public Guid ProjectId { get; set; }
    }
}
