using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetByUserPrimarySkillAsync(Guid projectId, DateTime? date, Guid primarySkillId);

        Task<ScheduleDTO> GetByDatePeriodForCurrentUserAsync(DateTime? date, int daysNum);

        Task BulkAppoinOrCanceltInterviewsAsync(IEnumerable<AppointInterviewDTO> appointInterviewDTOs);
    }
}
