using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class BulkCreateTestFeedbackDTO
    {
        public int Rating { get; set; }

        public IEnumerable<Guid> CandidatesIds { get; set; }

        public Guid ProjectId { get; set; }
    }
}
