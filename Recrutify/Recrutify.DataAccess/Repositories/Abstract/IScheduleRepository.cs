using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<List<Schedule>> GetByUserPrimarySkillAsync(IEnumerable<Guid> userIds,  DateTime date, Guid primarySkillId);

        Task<Schedule> GetByDatePeriodAsync(Guid userId, DateTime date, int daysNum);

        Task<Schedule> GetByUserIdAsync(Guid userId);
    }
}
