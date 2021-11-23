using System.Collections.Generic;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendQueueEmailService
    {
        void SendEmail(List<Candidate> candidates, StatusDTO status, Project project);
    }
}
