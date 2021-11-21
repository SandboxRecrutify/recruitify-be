using System.Collections.Generic;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendQueueEmailService
    {
        void SendEmail(List<CandidateDTO> candidates, Status status);
    }
}
