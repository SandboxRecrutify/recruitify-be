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

        public int TestRating
        {
            get => Feedbacks?.FirstOrDefault(x => x.Type == FeedbackTypeDTO.Test)?.Rating ?? 0;
            set { }
        }

        public int MentorFeedbackRating
        {
            get => Feedbacks?.FirstOrDefault(x => x.Type == FeedbackTypeDTO.Mentor)?.Rating ?? 0;
            set { }
        }

        public int InterviewRating
        {
            get => Feedbacks?.FirstOrDefault(x => x.Type == FeedbackTypeDTO.Interview)?.Rating ?? 0;
            set { }
        }

        public int TechInterviewOneStepRating
        {
            get => Feedbacks?.FirstOrDefault(x => x.Type == FeedbackTypeDTO.TechInterviewOneStep)?.Rating ?? 0;
            set { }
        }

        public int TechInterviewSecondStepRating
        {
            get => Feedbacks?.FirstOrDefault(x => x.Type == FeedbackTypeDTO.TechInterviewSecondStep)?.Rating ?? 0;
            set { }
        }

        public CandidatePrimarySkillDTO PrimarySkill { get; set; }
    }
}
