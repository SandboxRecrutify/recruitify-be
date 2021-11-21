using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class AssignedCandidateDTO
    {
        public IEnumerable<int> BestTimeToConnect { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public IEnumerable<ProjectResultAssignedDTO> ProjectResults { get; set; }
    }
}
