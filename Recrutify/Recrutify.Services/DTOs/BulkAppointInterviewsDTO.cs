using System;

namespace Recrutify.Services.DTOs
{
    public class BulkAppointInterviewsDTO
    {
        public Guid UserId { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime AppointDateTime { get; set; }
    }
}
