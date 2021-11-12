using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidateCreateDTO : BaseCandidateDTO
    {
        public CandidatePrimarySkillDTO PrimarySkill { get; set; }
    }
}
