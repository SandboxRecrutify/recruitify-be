using System;
using System.Collections.Generic;

namespace Recrutify.Host.Infrastructure
{
    public class ProjectRoles
    {
        public Guid ProjectId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
