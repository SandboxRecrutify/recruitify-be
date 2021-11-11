using System;
using System.Collections.Generic;

namespace Recrutify.Host.ProjectAuthorize
{
    public class ParseJsonToDict
    {
        public Guid ProjectId { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<string> Role { get; set; }
    }
}
