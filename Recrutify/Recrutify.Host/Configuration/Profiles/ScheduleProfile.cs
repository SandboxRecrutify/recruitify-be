using Recrutify.DataAccess.Models;
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
