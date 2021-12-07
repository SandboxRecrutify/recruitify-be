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

        Task BulkUpdateScheduleSlotsAsync(Guid userId, IEnumerable<DateTime> newListDateTime, IEnumerable<DateTime> removedListDateTime);

        Task<IEnumerable<ScheduleSlot>> GetScheduleSlotsOfDatePeriodAsync(Guid userId, DateTime periodStartDate, DateTime periodFinishDate);

        Task BulkСancelInterviewsAsync(IEnumerable<Interview> cancelledInterviews);

        Task BulkAppointInterviewsAsync(IEnumerable<Interview> appointedInterviews, IEnumerable<ScheduleCandidateInfo> candidatesInfo);

        Task<List<ScheduleShort>> GetNotFreeShuduleSlotsBySlotsAsync(Dictionary<Guid, List<ScheduleSlotShort>> slots);

        Task<List<ScheduleShort>> GetFreeShuduleSlotsBySlotsAsync(Dictionary<Guid, List<ScheduleSlotShort>> slots);
    }
}
