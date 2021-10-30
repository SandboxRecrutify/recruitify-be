using System;

namespace Recrutify.Services.DTOs
{
    public class ProjectPrimarySkillDTO
    {
        public Guid PrimarySkillId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TestLink { get; set; }
    }
}
