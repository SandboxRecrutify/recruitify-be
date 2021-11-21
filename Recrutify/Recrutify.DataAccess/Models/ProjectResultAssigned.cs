using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class ProjectResultAssigned
    {
        public Guid ProjectId { get; set; }

        public Status Status { get; set; }

        public bool IsAssigned { get; set; }

        public string Reason { get; set; }
        public CandidatePrimarySkill PrimarySkill { get; set; }
    }
}
