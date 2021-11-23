using System.Collections.Generic;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> GetEmailRequests(List<Candidate> candidates, Project project, string templatePath);
    }
}
