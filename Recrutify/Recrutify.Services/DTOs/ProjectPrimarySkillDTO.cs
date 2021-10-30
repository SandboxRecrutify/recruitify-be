using System;

namespace Recrutify.Services.DTOs
{
    public class ProjectPrimarySkillDTO
    {
        public Guid PrimarySkillId { get; set; }

        public string PrimarySkillName { get; set; }

        public string PrimarySkillDescription { get; set; }

        public string PrimarySkillTestLink { get; set; }
    }
}
