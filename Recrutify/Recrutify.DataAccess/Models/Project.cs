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

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

        public List<PrimarySkill> PrimarySkills { get; set; }

        public List<Staff> Staff { get; set; }

        public bool IsRecommended { get; set; }
    }
}
