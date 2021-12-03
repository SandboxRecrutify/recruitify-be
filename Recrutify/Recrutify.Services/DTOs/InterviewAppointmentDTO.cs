using System;

namespace Recrutify.Services.DTOs
{
    public class InterviewAppointmentDTO
    {
        public Guid UserId { get; set; }

        public bool IsAppointment { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime AppointmentDateTime { get; set; }
    }
}
