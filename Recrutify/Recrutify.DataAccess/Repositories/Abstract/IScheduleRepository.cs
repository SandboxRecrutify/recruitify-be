using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IScheduleRepository
    {
        Task<List<Schedule>> GetUsersSchedulesByPrimarySkillAsync(IEnumerable<Guid> interviewersIds,  DateTime date, Guid primarySkillId);
    }
}
