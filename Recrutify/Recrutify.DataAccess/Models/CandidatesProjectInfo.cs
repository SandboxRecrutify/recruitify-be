using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class CandidatesProjectInfo
    {
        public Guid? Id { get; set; }

        public IEnumerable<CandidatePrimarySkill> PrimarySkills { get; set; }

        public IEnumerable<Location> Locations { get; set; }

        public string ProjectName { get; set; }
    }
}
