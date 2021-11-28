using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class CandidatesPrimarySkillsAndLocation
    {
        public List<CandidatePrimarySkill> PrimarySkills { get; set; }

        public List<Location> Locations { get; set; }
    }
}
