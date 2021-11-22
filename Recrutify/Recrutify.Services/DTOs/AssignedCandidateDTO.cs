﻿using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class AssignedCandidateDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> BestTimeToConnect { get; set; }

        public string Email { get; set; }
        
        public IEnumerable<ProjectResultAssignedDTO> ProjectResults { get; set; }
    }
}
