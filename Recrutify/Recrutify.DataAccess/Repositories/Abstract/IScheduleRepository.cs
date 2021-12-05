using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IScheduleRepository
    {
        Task<List<Schedule>> GetByUserPrimarySkillAsync(IEnumerable<Guid> userIds,  DateTime date, Guid primarySkillId);

        Task UpdateScheduleSlotsCandidateInfoAsync(IEnumerable<InterviewAppointment> interviewAppointment);

        Task<Schedule> GetByDatePeriodAsync(Guid userId, DateTime date, int daysNum);

        Task BulkUpdateScheduleSlotsAsync(Guid userId, IEnumerable<DateTime> newListDateTime, IEnumerable<DateTime> removedListDateTime);

        Task<IEnumerable<ScheduleSlot>> GetScheduleSlotsOfDatePeriodAsync(Guid userId, DateTime periodStartDate, DateTime periodFinishDate);
    }
}
