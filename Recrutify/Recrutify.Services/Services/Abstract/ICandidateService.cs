using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ICandidateService
    {
        public Task<List<CandidateDTO>> GetAllAsync();

        public Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO);
    }
}
