﻿using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidateDTO : CandidateCreateDTO
    {
        public Guid Id { get; set; }

        public List<ProjectResultDTO> ProjectResults { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
