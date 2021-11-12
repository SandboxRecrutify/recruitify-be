using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class ProjectResult
    {
        public IEnumerable<Feedback> Feedbacks { get; set; }

        public Guid ProjectId { get; set; }

        public Status Status { get; set; }

        public string Reason { get; set; }

        public CandidatePrimarySkill PrimarySkill { get; set; }
    }
}
