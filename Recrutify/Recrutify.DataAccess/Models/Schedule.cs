using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class Schedule
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string UserEmail { get; set; }

        public UserPrimarySkill PrimarySkill { get; set; }

        public IEnumerable<AvailableTime> AvailableTimes { get; set; }
    }
}
