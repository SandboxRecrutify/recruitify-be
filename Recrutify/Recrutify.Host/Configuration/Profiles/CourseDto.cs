﻿using Recrutify.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Recrutify.Host.Configuration.Profiles
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

        public List<PrimarySkill> PrimarySkills { get; set; }
    }
}