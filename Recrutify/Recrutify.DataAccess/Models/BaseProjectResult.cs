using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class BaseProjectResult
    {
        public Guid ProjectId { get; set; }

        public Status Status { get; set; }

        public bool IsAssigned { get; set; }

        public CandidatePrimarySkill PrimarySkill { get; set; }
    }
}
