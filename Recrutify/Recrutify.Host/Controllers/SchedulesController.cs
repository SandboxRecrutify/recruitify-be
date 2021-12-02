using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet]
        public Task<IEnumerable<ScheduleDTO>> GetByUserPrimarySkillAsync([FromQuery, Required] Guid projectId, [FromQuery] DateTime? date, [FromQuery, Required] Guid primarySkillId)
        {
            return _scheduleService.GetByUserPrimarySkillAsync(projectId, date, primarySkillId);
        }

        [HttpGet("current_user")]
        public Task<ScheduleDTO> GetByDatePeriodForCurrentUserAsync([FromQuery] DateTime? date, [FromQuery] int daysNum = 1)
        {
            return _scheduleService.GetByDatePeriodForCurrentUserAsync(date, daysNum);
        }

        [HttpPut("current_user")]
        public async Task UpdateSlotsForCurrentUserAsync(IEnumerable<DateTime> timeSlots, DateTime mondayDate)
        {
            await _scheduleService.UpdateSlotsForCurrentUser(timeSlots, mondayDate);
        }
    }
}
