using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendQueueEmailService
    {
        Task SendEmail(List<CandidateDTO> candidates, StatusDTO status);
    }
}
