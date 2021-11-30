using System;

namespace Recrutify.DataAccess.Models
{
    public class AppointInterviewHelper
    {
        public ScheduleSlot ScheduleSlot { get; set; }

        public Candidate Candidate { get; set; }

        public Guid UserId { get; set; }
    }
}
