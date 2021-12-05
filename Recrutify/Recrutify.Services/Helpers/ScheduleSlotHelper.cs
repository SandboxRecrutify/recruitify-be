using System;
using System.Collections.Generic;
using System.Linq;
using Recrutify.Services.Helpers.Abstract;

namespace Recrutify.Services.Helpers
{
    public class ScheduleSlotHelper : IScheduleSlotHelper
    {
        public IEnumerable<DateTime> GetAddedDateTimeInSheduleSlots(IEnumerable<DateTime> currentDates, IEnumerable<DateTime> newDates)
        {
            return newDates.Where(x => !currentDates.Contains(x));
        }

        public IEnumerable<DateTime> GetRemovedDateTimeInSheduleSlots(IEnumerable<DateTime> currentDates, IEnumerable<DateTime> newDates)
        {
            return currentDates.Where(x => !newDates.Contains(x));
        }
    }
}
