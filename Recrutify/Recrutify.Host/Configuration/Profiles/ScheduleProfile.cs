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
            CreateMap<ScheduleSlot, ScheduleSlotShort>()
                .ForMember(dest => dest.CandidateId, conf => conf.MapFrom(src => src.ScheduleCandidateInfo.Id));

            CreateMap<ScheduleCandidateInfo, ScheduleCandidateInfoDTO>();

            CreateMap<InterviewDTO, ScheduleSlotShort>()
                .ForMember(dest => dest.AvailableTime, conf => conf.MapFrom(src => src.AppoitmentDateTime));

            CreateMap<Candidate, ScheduleCandidateInfo>()
                .ForMember(dest => dest.Skype, conf => conf.MapFrom(src => src.Contacts.FirstOrDefault(c => c.Type == Constants.Contacts.Skype).Value))
                .ForMember(dest => dest.ProjectResult, opt => opt.Ignore());

            CreateMap<ScheduleCandidateProjectResult, ScheduleCandidateProjectResultDTO>();

            CreateMap<Interview, InterviewDTO>().ReverseMap();

            CreateMap<ProjectResult, ScheduleCandidateProjectResult>();
        }
    }
}
