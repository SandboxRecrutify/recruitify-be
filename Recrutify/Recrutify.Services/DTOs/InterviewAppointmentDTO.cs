using System;

namespace Recrutify.Services.DTOs
{
    public class InterviewAppointmentDTO
    {
        public Guid UserId { get; set; }

        public bool IsAppoint { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime AppointDateTime { get; set; }
    }
}
