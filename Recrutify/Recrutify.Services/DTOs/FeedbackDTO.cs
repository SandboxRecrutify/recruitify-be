using System;

namespace Recrutify.Services.DTOs
{
    public class FeedbackDTO : UpsertFeedbackDTO
    {
        public DateTime CreatedOn { get; set; }
    }
}
