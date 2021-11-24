using System.Collections.Generic;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> GetEmailRequests(IEnumerable<Candidate> candidates, ProjectDTO project, string templatePath);
    }
}
