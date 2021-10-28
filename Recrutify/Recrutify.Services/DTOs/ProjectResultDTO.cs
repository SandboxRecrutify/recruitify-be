﻿using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class ProjectResultDTO
    {
        public List<FeedbackDTO> Feedbacks { get; set; }

        public Guid ProjectId { get; set; }

        public StatusDTO Status { get; set; }

        public string Cause { get; set; }
    }
}
