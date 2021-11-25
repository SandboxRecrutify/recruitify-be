using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class ScheduleSlotDTO
    {
        public DateTime AvailableTime { get; set; }

        public ScheduleCandidateInfoDTO ScheduleCandidateInfo { get; set; }
    }
}
