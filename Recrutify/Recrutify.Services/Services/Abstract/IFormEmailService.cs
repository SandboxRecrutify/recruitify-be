using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> FormDeclinedEmail(List<CandidateDTO> candidates, ProjectDTO project);

        IEnumerable<EmailRequest> FormAcceptanceEmail(List<CandidateDTO> candidates, ProjectDTO project);

        IEnumerable<EmailRequest> FormWaitingListEmail(List<CandidateDTO> candidates, ProjectDTO project);
    }
}
