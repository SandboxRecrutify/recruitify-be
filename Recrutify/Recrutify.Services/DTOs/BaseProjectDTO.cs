using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public abstract class BaseProjectDTO
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartRegistrationDate { get; set; }

        public DateTime EndRegistrationDate { get; set; }

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

        public IEnumerable<ProjectPrimarySkillDTO> PrimarySkills { get; set; }

        public bool IsActive { get; set; }
    }
}
