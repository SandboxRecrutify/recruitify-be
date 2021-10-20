using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Dtos
{
    public class CreateCourseDto
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

        public List<PrimarySkill> PrimarySkills { get; set; }
    }
}
