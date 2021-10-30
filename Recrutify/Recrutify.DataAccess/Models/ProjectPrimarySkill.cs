using System;

namespace Recrutify.DataAccess.Models
{
    public class ProjectPrimarySkill
    {
        public Guid PrimarySkillId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TestLink { get; set; }
    }
}