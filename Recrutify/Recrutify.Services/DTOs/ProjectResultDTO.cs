using System;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.Services.DTOs
{
    public class ProjectResultDTO
    {
        public Guid ProjectId { get; set; }

        public StatusDTO Status { get; set; }

        public bool IsAssignedOnInterview { get; set; }

        public string Reason { get; set; }

        public IEnumerable<FeedbackDTO> Feedbacks { get; set; }

        public CandidatePrimarySkillDTO PrimarySkill { get; set; }
    }
}
