﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetByUserPrimarySkillAsync(Guid projectId, DateTime date, Guid primarySkillId);

        Task BulkAppoinOrCanceltInterviewsAsync(IEnumerable<BulkAppointInterviewsDTO> bulkAppointInterviewsDTO, bool isUpdate);
    }
}
