using System.Collections.Generic;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ISendEmailQueueService
    {
        void SendEmailQueue(IEnumerable<Candidate> candidates, StatusDTO status, ProjectDTO project);
    }
}
