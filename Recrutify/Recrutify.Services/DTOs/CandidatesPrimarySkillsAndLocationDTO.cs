using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidatesPrimarySkillsAndLocationDTO
    {
        public List<CandidatePrimarySkillDTO> PrimarySkill { get; set; }

        public List<LocationDTO> Location { get; set; }
    }
}
