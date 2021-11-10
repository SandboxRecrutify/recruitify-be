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

        public async Task<Dictionary<Role, StaffDTO[]>> GetByRoleAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<Dictionary<Role, StaffDTO[]>>(
                        users.SelectMany(p => p.Roles, (user, role) => new { user, role })
                              .GroupBy(x => x.role)
                              .ToDictionary(k => k.Key, i => i.Select(b => b.user).ToArray()));
        }
    }
}
