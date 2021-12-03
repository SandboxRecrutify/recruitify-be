using System;

namespace Recrutify.Services.DTOs
{
    public class InterviewAppointmentSlotDTO
    {
        public ScheduleSlotDTO ScheduleSlot { get; set; }

        public Guid UserId { get; set; }

        public bool IsAppointment { get; set; }
    }
}
