using System;
using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> GetEmailRequestsForChangeStatus(IEnumerable<CandidateDTO> candidates, ProjectDTO project, string templatePath);

        IEnumerable<EmailRequest> GetEmailRequestsForInterviewInvite(IEnumerable<CandidateDTO> candidates, DateTime interviewTime, string templatePath, string interviewerRole);

    }
}
