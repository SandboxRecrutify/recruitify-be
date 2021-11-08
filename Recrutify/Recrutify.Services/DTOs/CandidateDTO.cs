using System;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.Services.DTOs
{
    public class CandidateDTO : CandidateCreateDTO
    {
        public Guid Id { get; set; }

        public IEnumerable<ProjectResultDTO> ProjectResults { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int SumRating(Guid projectId)
        {
            int s = 0;
            foreach (var e in this.ProjectResults.FirstOrDefault(x => x.ProjectId == projectId).Feedbacks)
            {
                if (e.Type != FeedbackTypeDTO.Test)
                {
                    s += e.Rating;
                }
            }

            return s;
        }

        public int TestResult(Guid projectId)
        {
            int s = 0;
            foreach (var e in this.ProjectResults.FirstOrDefault(x => x.ProjectId == projectId).Feedbacks)
            {
                if (e.Type == 0)
                {
                    s = e.Rating;
                    break;
                }
            }

            return s;
        }
    }
}
