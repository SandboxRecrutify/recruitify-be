using System;

namespace Recrutify.Services.DTOs
{
    public class FeedbackDTO
    {
        public string TextFeedback { get; set; }

        public int Rating { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public FeedbackTypeDTO Type { get; set; }

        public bool IsRecommended { get; set; }
    }
}
