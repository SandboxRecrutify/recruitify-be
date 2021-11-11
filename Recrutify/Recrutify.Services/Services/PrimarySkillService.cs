using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class PrimarySkillService : IPrimarySkillService
    {
        private readonly IPrimarySkillRepository _primarySkillRepository;
        private readonly IMapper _mapper;

        public PrimarySkillService(IPrimarySkillRepository primarySkillRepository, IMapper mapper)
        {
            _primarySkillRepository = primarySkillRepository;
            _mapper = mapper;
        }

        public async Task<List<PrimarySkillDTO>> GetAllAsync()
        {
            var skills = await _primarySkillRepository.GetAllAsync();
            return _mapper.Map<List<PrimarySkillDTO>>(skills);
        }
    }
}
