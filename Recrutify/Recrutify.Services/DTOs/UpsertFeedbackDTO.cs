namespace Recrutify.Services.DTOs
{
    public class UpsertFeedbackDTO
    {
        public string TextFeedback { get; set; }

        public int Rating { get; set; }

        public FeedbackTypeDTO Type { get; set; }
    }
}
