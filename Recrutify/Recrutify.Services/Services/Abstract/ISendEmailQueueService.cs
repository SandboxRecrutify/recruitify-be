using System;
using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailQueueService
    {
        void SendEmailQueueForStatusChange(IEnumerable<CandidateDTO> candidates, StatusDTO status, ProjectDTO project);

        void SendEmailQueueForTest(IEnumerable<CandidateDTO> candidates, ProjectDTO project, DateTime testDeadlineDate, string emailToContact);

        void SendEmailQueueForInvite(IEnumerable<InterviewEmailInfo> interviews);
    }
}
