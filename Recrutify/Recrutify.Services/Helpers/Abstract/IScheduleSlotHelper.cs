using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Helpers.Abstract
{
    public interface IScheduleSlotHelper
    {
        IEnumerable<DateTime> GetAddedDateTimeInSheduleSlots(IEnumerable<DateTime> currentDates, IEnumerable<DateTime> newDates);

        IEnumerable<DateTime> GetRemovedDateTimeInSheduleSlots(IEnumerable<DateTime> currentDates, IEnumerable<DateTime> newDates);
    }
}
