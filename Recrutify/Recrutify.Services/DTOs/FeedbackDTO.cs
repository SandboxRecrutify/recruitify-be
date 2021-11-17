using System;

namespace Recrutify.Services.DTOs
{
    public class FeedbackDTO : UpsertFeedbackDTO
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
