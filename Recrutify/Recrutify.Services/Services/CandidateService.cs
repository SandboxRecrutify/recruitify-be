using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.AccessData.Repositories;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }
        public async Task<List<Candidate>> GetAllAsync()
        {

        }
    }
}
