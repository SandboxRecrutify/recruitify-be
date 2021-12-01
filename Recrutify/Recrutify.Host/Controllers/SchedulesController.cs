using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Events;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IInviteEventPublisher _inviteEventPublisher;
        private readonly IUpdateStatusEventPublisher _updateStatusEventPublisher;
        public SchedulesController(IScheduleService scheduleService, IInviteEventPublisher inviteEventPublisher, IUpdateStatusEventPublisher updateStatusEventPublisher)
        { 
            _scheduleService = scheduleService;
            _inviteEventPublisher = inviteEventPublisher;
            _updateStatusEventPublisher = updateStatusEventPublisher;
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

        [HttpGet("test")]
        public void Test()
        {
            List<Interview> interviews = new List<Interview>();
            interviews.Add(new Interview()
            {
                AppointDateTimeUtc = DateTime.Now.AddDays(1),
                InterviewType = InterviewType.TerchicalInterviewSecond,
                Candidate = new CandidateEmailInfo() { Email = "evgentik@mail.ru", Name = "Евгений", PhoneNumber="+375336360000", Skype="evgen" },
                User = new UserEmailInfo() { Email = "mosikevgenii@gmail.com", Name = "Джо" },
            });

            AssignedInterviewEventArgs args = new AssignedInterviewEventArgs() { Interviews = interviews };

            _inviteEventPublisher.OnAssignedInterview(args);
        }
    }
}
