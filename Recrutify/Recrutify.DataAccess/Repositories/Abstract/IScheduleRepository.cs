using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IScheduleRepository
    {
        Task<List<Schedule>> GetByUserPrimarySkillAsync(IEnumerable<Guid> userIds,  DateTime date, Guid primarySkillId);

        Task<Schedule> GetByDatePeriodAsync(Guid userId, DateTime date, int daysNum);

        Task BulkUpdateScheduleSlotAsync(Guid userId, IEnumerable<DateTime> newScheduleSlot, IEnumerable<DateTime> remoteScheduleSlot);

        Task<IEnumerable<ScheduleSlot>> GetScheduleSlotsByDatePeriodAsync(Guid userId, DateTime dateStartPeriod);
    }
}
