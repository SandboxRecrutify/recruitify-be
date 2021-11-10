using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<StaffGroupDTO> GetByGroupRoleAsync()
        {
            var users = await _userRepository.GetByRolesAsync();

            var staff = _mapper.Map<Dictionary<Role, List<StaffDTO>>>(
                        users.SelectMany(p => p.Roles, (user, role) => new { user, role })
                              .GroupBy(x => x.role)
                              .ToDictionary(k => k.Key, i => i.Select(b => b.user).ToList()));

            var result = new StaffGroupDTO()
            {
                Managers = staff.TryGetValue(key: Role.Manager, value: out var ma) ? ma : default,
                Interviewers = staff.TryGetValue(key: Role.Interviewer, value: out var i) ? i : default,
                Recruiters = staff.TryGetValue(key: Role.Recruiter, value: out var r) ? r : default,
                Mentors = staff.TryGetValue(key: Role.Mentor, value: out var me) ? me : default,
            };

            return result;
        }
    }
}
