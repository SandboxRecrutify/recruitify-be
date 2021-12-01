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
        private readonly ISendEmailQueueService _sendEmailQueueService;

        public SchedulesController(IScheduleService scheduleService, ISendEmailQueueService sendEmailQueueService)
        {
            _scheduleService = scheduleService;
            _sendEmailQueueService = sendEmailQueueService;
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
                AppointDateTime = DateTime.Now.AddDays(10),
                Candidate = new CandidateByEmail() { Email = "mosikevgenii@gmail.com", Name = "Евген" },
                User = new UserByEmail() { Email = "evgentik@mail.ru", Name = "Евген М" },
            });
            _sendEmailQueueService.SendEmailQueueForInvite(interviews);
        }
    }
}
