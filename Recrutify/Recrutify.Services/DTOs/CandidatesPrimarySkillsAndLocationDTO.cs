using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidatesPrimarySkillsAndLocationDTO
    {
        public List<CandidatePrimarySkillDTO> PrimarySkills { get; set; }

        public List<LocationDTO> Locations { get; set; }
    }
}
