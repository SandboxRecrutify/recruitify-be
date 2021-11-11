using System;

namespace Recrutify.DataAccess.Models
{
    public class ProjectPrimarySkill
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TestLink { get; set; }
    }
}
