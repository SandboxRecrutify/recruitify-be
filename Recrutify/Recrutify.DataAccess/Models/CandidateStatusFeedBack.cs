using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class CandidateStatusFeedBack
    {
        public Status Status { get; set; }

        public IEnumerable<Feedback> Feedback { get; set; }

        public Guid ProjectId { get; set; }

        public string Reason { get; set; }
    }
}
