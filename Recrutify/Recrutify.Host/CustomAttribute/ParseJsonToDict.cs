using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recrutify.Host.CustomAttribute
{
    public class ParseJsonToDict
    {
        [JsonProperty(PropertyName = "projectId")]
        public Guid ProjectId { get; set; }

        [JsonProperty(PropertyName = "roles")]
        public IEnumerable<string> Role { get; set; }
    }
}
