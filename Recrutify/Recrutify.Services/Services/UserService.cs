using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess;
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

        public Task<Dictionary<Guid, string>> GetNamesByIdsAsync(IEnumerable<Guid> ids)
        {
            return _userRepository.GetNamesByIdsAsync(ids);
        }

        public async Task<StaffGroupDTO> GetStaffByRolesAsync(IEnumerable<Role> roles)
        {
            var users = await _userRepository.GetByRoles(roles);

            var staff = _mapper.Map<Dictionary<Role, List<StaffDTO>>>(
                        users.SelectMany(p => p.ProjectRoles[Constants.GlobalProject.GlobalProjectId], (user, role) => new { user, role })
                             .GroupBy(x => x.role)
                             .ToDictionary(k => k.Key, i => i.Select(b => b.user).ToList()));

            var result = new StaffGroupDTO()
            {
                Managers = staff.TryGetValue(key: Role.Manager, value: out var managers) ? managers : default,
                Interviewers = staff.TryGetValue(key: Role.Interviewer, value: out var interviewers) ? interviewers : default,
                Recruiters = staff.TryGetValue(key: Role.Recruiter, value: out var recruiters) ? recruiters : default,
                Mentors = staff.TryGetValue(key: Role.Mentor, value: out var mentors) ? mentors : default,
            };

            return result;
        }

        public Task BulkAddProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> usersRoles)
        {
            return _userRepository.BulkAddProjectRolesAsync(projectId, usersRoles);
        }

        public void BulkUpdateProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles)
        {
            #region Helper
            var newUsers = newUsersRoles.Where(x => !currentUsersRoles.Keys.Contains(x.Key)).ToDictionary(x=>x.Key, x=>x.Value);
            var remoteUsers = currentUsersRoles.Where(x => !newUsersRoles.Keys.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            var updateUsers = newUsersRoles
                                .Where(x =>
                                    currentUsersRoles.Keys.Contains(x.Key) &&
                                    (
                                        x.Value.Any(m => !currentUsersRoles[x.Key].Contains(m)) |
                                        currentUsersRoles[x.Key].Any(m => !x.Value.Contains(m)))).ToDictionary(x => x.Key, x => x.Value);
            #endregion

            //if(newUsers!=null)
            //    _userRepository.BulkAddProjectRolesAsync(projectId, newUsers);

            //if(remoteUsers!=null)
            //    _userRepository.BulkRemoveProjectRolesAsync(projectId, remoteUsers);

            if(updateUsers!=null)
                _userRepository.BulkUpdateProjectRolesAsync(projectId, updateUsers);




        }

       
    }
}
