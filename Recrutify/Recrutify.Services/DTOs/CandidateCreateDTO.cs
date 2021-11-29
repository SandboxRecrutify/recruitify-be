using System;

namespace Recrutify.Services.DTOs
{
    public class CandidateCreateDTO : BaseCandidateDTO
    {
        public Guid PrimarySkillId { get; set; }
    }
}
