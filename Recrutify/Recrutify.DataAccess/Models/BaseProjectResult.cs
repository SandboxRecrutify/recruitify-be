﻿using System;

namespace Recrutify.DataAccess.Models
{
    public abstract class BaseProjectResult
    {
        public Guid ProjectId { get; set; }

        public Status Status { get; set; }

        public bool IsAssigned { get; set; }

        public CandidatePrimarySkill PrimarySkill { get; set; }
    }
}
