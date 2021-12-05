using System;

namespace Recrutify.DataAccess.Models
{
    public class InterviewAppointment
    {
        public ScheduleSlot ScheduleSlot { get; set; }

        public Guid UserId { get; set; }

        public bool IsAppointment { get; set; }
    }
}
