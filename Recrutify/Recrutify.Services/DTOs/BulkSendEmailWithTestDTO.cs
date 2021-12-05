using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class BulkSendEmailWithTestDTO
    {
        public IEnumerable<Guid> CandidatesIds { get; set; }

        public Guid ProjectId { get; set; }

        public string PersonToContactEmail { get; set; }

        public DateTime TestDeadlineDate { get; set; }
    }
}
