using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class CreateFeedbackDTO
    {
        public string TextFeedback { get; set; }

        public int Rating { get; set; }

        public Guid UserId { get; set; }

        public FeedbackTypeDTO Type { get; set; }

        public bool IsRecommended { get; set; }
    }
}
