using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class ProjectResultAssignedDTO
    {
        public Guid ProjectId { get; set; }

        public StatusDTO Status { get; set; }

        public bool IsAssigned { get; set; }

        public string Reason { get; set; }
        public CandidatePrimarySkillDTO PrimarySkill { get; set; }

    }
}
