using System;
using System.Collections.Generic;
using System.Linq;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Extensions
{
    public static class StaffExtensions
    {
        public static IEnumerable<Staff> GetStaff(this IEnumerable<Guid> userIds,  Dictionary<Guid, string> users)
        {
            var staff = userIds.Select(u => new Staff()
            {
                UserId = u,
                UserName = users.TryGetValue(u, out var id) ? id : default,
            }).Where(u => u.UserName != null);
            return staff;
        }
    }
}
