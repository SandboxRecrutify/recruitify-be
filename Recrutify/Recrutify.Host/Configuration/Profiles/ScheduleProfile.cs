using System.Linq;
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

            CreateMap<Candidate, ScheduleCandidateInfo>()
                .ForMember(dest => dest.Skype, conf => conf.MapFrom(src => src.Contacts.FirstOrDefault(c => c.Type == Constants.Contacts.Skype).Value))
                .ForMember(dest => dest.ProjectResult, opt => opt.Ignore());

            CreateMap<ScheduleCandidateProjectResult, ScheduleCandidateProjectResultDTO>();

            CreateMap<Interview, InterviewDTO>().ReverseMap();

            CreateMap<ProjectResult, ScheduleCandidateProjectResult>();
        }
    }
}
