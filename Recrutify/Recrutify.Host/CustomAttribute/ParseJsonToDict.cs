using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recrutify.Host.CustomAttribute
{
    public class ParseJsonToDict
    {
        public Guid ProjectId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
