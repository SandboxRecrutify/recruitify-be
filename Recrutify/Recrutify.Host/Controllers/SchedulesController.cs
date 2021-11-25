using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("users_schedules/{projectId:guid}/{primarySkillId:guid}")]
        public Task<IEnumerable<ScheduleDTO>> GetUsersSchedulesByPrimarySkillAsync(Guid projectId, DateTime date, Guid primarySkillId)
        {
            return _scheduleService.GetUsersSchedulesByPrimarySkillAsync(projectId, date, primarySkillId);
        }
    }
}
