using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
    }
}
 