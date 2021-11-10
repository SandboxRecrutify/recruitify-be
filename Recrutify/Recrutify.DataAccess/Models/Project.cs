using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess
{
    public class Project : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartRegistrationDate { get; set; }

        public DateTime EndRegistrationDate { get; set; }

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

        public IEnumerable<ProjectPrimarySkill> PrimarySkills { get; set; }

        public IEnumerable<Staff> Managers { get; set; }

        public IEnumerable<Staff> Interviewers { get; set; }

        public IEnumerable<Staff> Recruiters { get; set; }

        public IEnumerable<Staff> Mentors { get; set; }

        public bool IsActive { get; set; }
    }
}
