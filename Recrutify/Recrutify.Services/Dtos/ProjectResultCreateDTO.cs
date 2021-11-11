using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.DTOs
{
    public class ProjectResultCreateDTO
    {
        public CandidatePrimarySkillDTO PrimarySkillDTO { get; set; }

        public IEnumerable<FeedbackDTO> Feedbacks { get; set; }

        public Guid ProjectId { get; set; }

        public StatusDTO Status { get; set; }

        public string Reason { get; set; }
    }
}
