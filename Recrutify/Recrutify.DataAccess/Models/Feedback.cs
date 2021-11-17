using System;

namespace Recrutify.DataAccess.Models
{
    public class Feedback
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string TextFeedback { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public FeedbackType Type { get; set; }
    }
}
