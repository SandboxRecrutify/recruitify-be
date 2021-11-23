using System;

namespace Recrutify.DataAccess.Models
{
    public class ScheduleSlot
    {
        public DateTime AvailableTime { get; set; }

        public ScheduleCandidateInfo AssignedCandidate { get; set; }
    }
}
