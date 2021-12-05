using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;

namespace Recrutify.Host.Configuration.Profiles
{
    public class ScheduleProfile : AutoMapper.Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDTO>();

            CreateMap<UserPrimarySkill, UserPrimarySkillDTO>();
            CreateMap<ScheduleSlot, ScheduleSlotDTO>().ReverseMap();

            CreateMap<ScheduleCandidateInfo, ScheduleCandidateInfoDTO>().ReverseMap();

            CreateMap<ScheduleCandidateProjectResult, ScheduleCandidateProjectResultDTO>().ReverseMap();

            CreateMap<CandidateDTO, Interview>()
                .ForMember(dest => dest.Candidate, conf => conf.MapFrom(src => new CandidateEmailInfo()
                {
                    Id = src.Id,
                    Email = src.Email,
                    Name = src.Name,
                    PhoneNumber = src.PhoneNumber,
                }))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.InterviewType, opt => opt.Ignore())
                .ForMember(dest => dest.AppointDateTimeUtc, opt => opt.Ignore());
        }
    }
}
