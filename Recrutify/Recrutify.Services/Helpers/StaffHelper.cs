using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.Helpers.Abstract;

namespace Recrutify.Services.Helpers
{
    public class StaffHelper : IStaffHelper
    {
        public IDictionary<Guid, IEnumerable<Role>> GetAddedUsersRoles(IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles)
        {
            return newUsersRoles.Where(x => !currentUsersRoles.Keys.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        public IDictionary<Guid, IEnumerable<Role>> GetRemovedUsersRoles(IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles)
        {
            return currentUsersRoles.Where(x => !newUsersRoles.Keys.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        public IDictionary<Guid, IEnumerable<Role>> GetUpdatedUsersRoles(IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles)
        {
            return newUsersRoles
                            .Where(x =>
                                currentUsersRoles.Keys.Contains(x.Key) &&
                                !x.Value.SequenceEqual(currentUsersRoles[x.Key]))
                            .ToDictionary(x => x.Key, x => x.Value);
        }

        public IDictionary<Guid, IEnumerable<Role>> GetStaffUsersByRoles(Project project)
        {
            var allStaff = project.Interviewers.Select(u => new { u.UserId, Role = Role.Interviewer })
                    .Union(project.Managers.Select(u => new { u.UserId, Role = Role.Manager }))
                    .Union(project.Recruiters.Select(u => new { u.UserId, Role = Role.Recruiter }))
                    .Union(project.Mentors.Select(u => new { u.UserId, Role = Role.Mentor }));
            return allStaff.GroupBy(g => g.UserId).ToDictionary(g => g.Key, g => g.Select(r => r.Role));
        }
    }
}
