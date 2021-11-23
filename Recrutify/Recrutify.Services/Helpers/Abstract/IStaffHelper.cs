using System;
using System.Collections.Generic;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Helpers.Abstract
{
    public interface IStaffHelper
    {
        IDictionary<Guid, IEnumerable<Role>> GetAddedUsersRoles(IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles);

        IDictionary<Guid, IEnumerable<Role>> GetRemovedUsersRoles(IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles);

        IDictionary<Guid, IEnumerable<Role>> GetUpdatedUsersRoles(IDictionary<Guid, IEnumerable<Role>> currentUsersRoles, IDictionary<Guid, IEnumerable<Role>> newUsersRoles);

        IDictionary<Guid, IEnumerable<Role>> GetStaffUsersByRoles(Project project);
    }
}
