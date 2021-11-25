﻿using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class Schedule : IDataModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public UserPrimarySkill UserPrimarySkill { get; set; }

        public IEnumerable<ScheduleSlot> ScheduleSlots { get; set; }
    }
}
