using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recrutify.Services.Dtos;

namespace Recrutify.Services.Services.Abstract
{
    public interface ICandidateService
    {
        public Task<List<CandidateDTO>> GetAllAsync();
    }
}
