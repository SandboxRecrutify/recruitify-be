using System;

namespace Recrutify.DataAccess.Models
{
    public class InterviewAppointmentSlot
    {
        public ScheduleSlot ScheduleSlot { get; set; }

        public Guid UserId { get; set; }

        public bool IsApponint { get; set; }
    }
}
