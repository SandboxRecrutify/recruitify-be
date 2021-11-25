using Recrutify.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration.Profiles
{
    public class ScheduleProfile : AutoMapper.Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<UserPrimarySkill, UserPrimarySkillDTO>();
            CreateMap<ScheduleSlot, ScheduleSlotDTO>();
            CreateMap<ScheduleCandidateInfo, ScheduleCandidateInfoDTO>();
            CreateMap<ScheduleCandidateProjectResult, ScheduleCandidateProjectResultDTO>();
        }
    }
}
