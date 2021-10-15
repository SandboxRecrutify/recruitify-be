﻿using Recrutify.DataAccess.Models;
using System;

namespace Recrutify.DataAccess
{
    /// <summary>
    /// Сollection Course in database.
    /// </summary>
    public class Course : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CurrentApplicationsCount { get; set; }

        public int PlannedApplicationsCount { get; set; }

        public string Description { get; set; }

    }
}
