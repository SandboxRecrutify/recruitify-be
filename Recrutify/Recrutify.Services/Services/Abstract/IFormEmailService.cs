﻿using System.Collections.Generic;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Services.Abstract
{
    public interface IFormEmailService
    {
        IEnumerable<EmailRequest> FormEmail(List<CandidateDTO> candidates, ProjectDTO project, string templatePath);
    }
}
