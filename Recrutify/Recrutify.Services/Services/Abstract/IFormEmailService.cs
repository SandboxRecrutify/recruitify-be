using System.Collections.Generic;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> GetEmailRequestsForStatusChange(IEnumerable<CandidateDTO> candidates, ProjectDTO project, string templatePath);

        IEnumerable<EmailRequest> GetEmailRequestsForSendTest(IEnumerable<CandidateDTO> candidates, ProjectDTO project);

        IEnumerable<EmailRequestForInvite> GetEmailRequestsForInterviewInvite(IEnumerable<Interview> interviews);
    }
}
