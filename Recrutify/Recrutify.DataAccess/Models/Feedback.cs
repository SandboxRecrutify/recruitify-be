using System;

namespace Recrutify.DataAccess.Models
{
    public class Feedback
    {
        public string TextFeedback { get; set; }

        public int Rating { get; set; }

        public Guid UserID { get; set; }
    }
}
