using System.Collections.Generic;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailQueueService
    {
        void SendEmailQueueForChangeStatus(IEnumerable<CandidateDTO> candidates, StatusDTO status, ProjectDTO project);
    }
}
