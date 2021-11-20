﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IUserService
    {
        public Task<StaffGroupDTO> GetStaffByRolesAsync(IEnumerable<Role> roles);

        Task<Dictionary<Guid, string>> GetNamesByIdsAsync(IEnumerable<Guid> ids);

        Task BulkAddProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> users);
    }
}
