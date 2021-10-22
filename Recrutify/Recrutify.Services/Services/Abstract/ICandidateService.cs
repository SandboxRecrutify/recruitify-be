using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Services.Abstract
{
    interface ICandidateService
    {
        Task<List<CandidateDTO>> GetAllAsync();
    }
}
