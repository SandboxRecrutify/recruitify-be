using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.DTOs
{
    public class ProjectCreateDTO
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

        public List<PrimarySkill> PrimarySkills { get; set; }

        public List<StaffDTO> Managers { get; set; }

        public List<StaffDTO> Interviewers { get; set; }

        public List<StaffDTO> Recruiters { get; set; }

        public List<StaffDTO> Mentors { get; set; }

        public bool IsRecommended { get; set; }
    }
}
