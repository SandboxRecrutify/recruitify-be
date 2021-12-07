using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Exceptions;
using Recrutify.Services.Services.Abstract;
using ValidationException = FluentValidation.ValidationException;

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

        [Authorize(Policy = Constants.Policies.FeedbackPolicy)]
        [HttpPut]
        public async Task<ActionResult> UpdateScheduleSlotsAsync(IEnumerable<DateTime> dates, DateTime? weekStart)
        {
            try
            {
                await _scheduleService.UpdateScheduleSlotsForCurrentUserAsync(dates, weekStart);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(new ValidationProblemDetails(
                      ex.Errors
                       .GroupBy(o => o.PropertyName)
                       .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())));
            }

            return NoContent();
        }

        [Authorize(Policy = Constants.Policies.FeedbackPolicy)]
        [HttpPut("bulk/update_assigned_interviews")]
        public async Task<ActionResult> BulkAppointOrCancelInterviewsAsync([FromBody] IEnumerable<InterviewDTO> interviews, [FromQuery, Required] Guid projectId, CancellationToken cancellationToken)
        {
            try
            {
                await _scheduleService.ProcessingAppointOrCancelInterviewsAsync(interviews, projectId, cancellationToken);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(new ValidationProblemDetails(
                      ex.Errors
                       .GroupBy(o => o.PropertyName)
                       .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())));
            }

            return NoContent();
        }
    }
}
