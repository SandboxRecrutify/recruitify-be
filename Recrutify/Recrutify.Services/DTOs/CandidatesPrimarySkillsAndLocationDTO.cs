using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidatesPrimarySkillsAndLocationDTO
    {
        public Guid? Id { get; set; }
        public IEnumerable<CandidatePrimarySkillDTO> PrimarySkills { get; set; }

        public IEnumerable<LocationDTO> Locations { get; set; }
    }
}
