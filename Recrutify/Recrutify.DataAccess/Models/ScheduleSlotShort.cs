using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class ScheduleSlotShort
    {
        public DateTime AvailableTime { get; set; }

        public Guid CandidateId { get; set; }
    }
}
