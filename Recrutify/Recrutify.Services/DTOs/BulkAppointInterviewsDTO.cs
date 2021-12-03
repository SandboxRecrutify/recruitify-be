using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class BulkAppointInterviewsDTO
    {
        public IEnumerable<InterviewAppointmentDTO> InterviewAppointments { get; set; }

        public Guid ProjectId { get; set; }
    }
}
