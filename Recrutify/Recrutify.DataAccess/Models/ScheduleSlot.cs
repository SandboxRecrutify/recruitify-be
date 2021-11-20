using System;

namespace Recrutify.DataAccess.Models
{
    public class ScheduleSlot
    {
        public DateTime AvailableTime { get; set; }

        public AssignedCandidate AssignedCandidate { get; set; }
    }
}
