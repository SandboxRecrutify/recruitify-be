using System;

namespace Recrutify.DataAccess.Models
{
    public class CandidateRenewal
    {
        public Status Status { get; set; }

        public bool IsAssignedOnInterview { get; set; }

        public Guid CandidateId { get; set; }
    }
}
