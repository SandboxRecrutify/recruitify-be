﻿using System;

namespace Recrutify.Services.DTOs
{
    public class ScheduleCandidateProjectResultDTO
    {
        public Guid ProjectId { get; set; }

        public StatusDTO Status { get; set; }

        public bool IsAssigned { get; set; }

        public CandidatePrimarySkillDTO PrimarySkill { get; set; }
    }
}
