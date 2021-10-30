using System;

namespace Recrutify.DataAccess.Models
{
    public class ProjectPrimarySkill
    {
        public Guid PrimarySkillId { get; set; }

        public string PrimarySkillName { get; set; }

        public string PrimarySkillDescription { get; set; }

        public string PrimarySkillTestLink { get; set; }
    }
}