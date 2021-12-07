using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class ScheduleShort
    {
        public Guid UserId { get; set; }

        public IEnumerable<ScheduleSlotShort> ScheduleSlots { get; set; }
    }
}
