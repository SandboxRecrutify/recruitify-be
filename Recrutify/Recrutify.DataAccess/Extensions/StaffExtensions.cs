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
            var staff = userIds.Select(x => new Staff()
            {
                UserId = x,
                UserName = users.TryGetValue(x, out var id) ? id : default,
            }).Where(x => x.UserName != null);
            return staff;
        }
    }
}
