using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class ParticipantSlot
    {
        public IEnumerable<int> BestTimeToConnectCanditate { get; set; }

        public Guid CandidateId { get; set; }

        public string CandidateName { get; set; }

        public Status CandidateStatus { get; set; }

        public Guid ProjectId { get; set; }

        public string CandidateEmail { get; set; }

        public string Skype { get; set; }

        public CandidatePrimarySkill PrimarySkill { get; set; }
    }
}
