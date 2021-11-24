using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class UserSchedule : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public UserPrimarySkill PrimarySkill { get; set; }

        public IEnumerable<ScheduleSlot> ScheduleSlots { get; set; }
    }
}
