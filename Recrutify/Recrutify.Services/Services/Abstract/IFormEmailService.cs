using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> FormDeclinedEmail(List<CandidateDTO> candidates);

        IEnumerable<EmailRequest> FormAcceptanceEmail(List<CandidateDTO> candidates);

        IEnumerable<EmailRequest> FormWaitingListEmail(List<CandidateDTO> candidates);
    }
}
