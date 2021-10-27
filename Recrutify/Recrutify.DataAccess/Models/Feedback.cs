using System;

namespace Recrutify.DataAccess.Models
{
    public class Feedback
    {
        public string TextFeedback { get; set; }

        public int Rating { get; set; }

        public Guid UserId { get; set; }

        public DateTime Created { get; set; }

        public FeedbackType Type { get; set; }

        public bool IsRecommended { get; set; }
    }
}
