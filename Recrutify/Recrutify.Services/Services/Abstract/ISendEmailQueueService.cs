﻿using System.Collections.Generic;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailQueueService
    {
        void SendEmailQueue(IEnumerable<CandidateDTO> candidates, StatusDTO status, ProjectDTO project);

        void SendEmailQueueForTest(IEnumerable<CandidateDTO> candidates, ProjectDTO project);
    }
}
