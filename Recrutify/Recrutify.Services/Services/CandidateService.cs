using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task<List<CandidateDTO>> GetAllAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return _mapper.Map<List<CandidateDTO>>(candidates);
        }
    }
}
