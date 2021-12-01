using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidatesProjectInfoDTO
    {
        public Guid? Id { get; set; }

        public IEnumerable<CandidatePrimarySkillDTO> PrimarySkills { get; set; }

        public IEnumerable<LocationDTO> Locations { get; set; }

        public string ProjectName { get; set; }
    }
}
