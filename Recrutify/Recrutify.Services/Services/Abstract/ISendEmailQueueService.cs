﻿using System.Collections.Generic;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailQueueService
    {
        void SendEmailQueueForStatusChange(IEnumerable<CandidateDTO> candidates, StatusDTO status, ProjectDTO project);

        void SendEmailQueueForTest(IEnumerable<CandidateDTO> candidates, ProjectDTO project);

        void SendEmailQueueForInvite(IEnumerable<Interview> interviews);
    }
}
