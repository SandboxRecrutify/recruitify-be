using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class TimeSlotsForUserDTO
    {
        public Guid UserId { get; set; }

        public IEnumerable<DateTime> TimeSlots { get; set; }

        public DateTime MondayDate { get; set; }
    }
}
