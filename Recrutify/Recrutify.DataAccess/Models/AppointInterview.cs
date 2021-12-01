using System;

namespace Recrutify.DataAccess.Models
{
    public class AppointInterview
    {
        public ScheduleSlot ScheduleSlot { get; set; }

        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }
    }
}
