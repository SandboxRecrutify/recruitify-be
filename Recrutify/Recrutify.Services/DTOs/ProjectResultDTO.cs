using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class ProjectResultDTO
    {
        public IEnumerable<FeedbackDTO> Feedbacks { get; set; }

        public Guid ProjectId { get; set; }

        public StatusDTO Status { get; set; }

        public string Reason { get; set; }

        public CandidatePrimarySkillDTO PrimarySkill { get; set; }
    }
}
