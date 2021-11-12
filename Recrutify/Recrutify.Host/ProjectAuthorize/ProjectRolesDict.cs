using System;
using System.Collections.Generic;

namespace Recrutify.Host.ProjectAuthorize
{
    public class ProjectRolesDict
    {
        public Guid ProjectId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
