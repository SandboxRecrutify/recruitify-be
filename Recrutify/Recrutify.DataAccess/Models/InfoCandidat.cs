using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class InfoCandidat
    {
        public IEnumerable<int> BestTimeToConnectCanditat { get; set; }

        public Guid CandidatId { get; set; }

        public string CandidatName { get; set; }

        public Status CandidatStatus { get; set; }

        public Guid ProjectId { get; set; }

        public string Email { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }

        public CandidatePrimarySkill PrimarySkill { get; set; }
    }
}
