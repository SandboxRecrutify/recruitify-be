using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
     public interface IFormAcceptanceEmailService
    {
        IEnumerable<EmailRequest> GetEmailRequests(List<CandidateDTO> candidates);
    }
}
