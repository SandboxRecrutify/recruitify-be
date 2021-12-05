using System;
using System.Linq;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration.Profiles
{
    public class CandidateProfile : AutoMapper.Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDTO>();

            CreateMap<CandidateCreateDTO, Candidate>()
                .ForMember(dest => dest.ProjectResults, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, conf => conf.MapFrom(src => DateTime.UtcNow.Date))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();

            CreateMap<CandidatesProjectInfo, CandidatesProjectInfoDTO>();

            CreateMap<ProjectResult, ProjectResultDTO>()
                .ForMember(dest => dest.PrimarySkill, conf => conf.MapFrom(src => new CandidatePrimarySkillDTO { Id = src.PrimarySkill.Id, Name = src.PrimarySkill.Name }));

            CreateMap<ProjectResult, ScheduleCandidateProjectResultDTO>()
                .ForMember(dest => dest.PrimarySkill, conf => conf.MapFrom(src => new CandidatePrimarySkillDTO { Id = src.PrimarySkill.Id, Name = src.PrimarySkill.Name }));

            CreateMap<UpsertFeedbackDTO, Feedback>()
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore());
            CreateMap<Feedback, FeedbackDTO>();

            CreateMap<ContactDTO, Contact>().ReverseMap();

            CreateMap<LocationDTO, Location>().ReverseMap();

            CreateMap<Candidate, ScheduleCandidateInfoDTO>()
                .ForMember(dest => dest.ProjectResult, opt => opt.Ignore())
                .ForMember(dest => dest.Skype, conf => conf.MapFrom(src => src.Contacts.FirstOrDefault(c => c.Type == DataAccess.Constants.Contacts.Skype).Value));
            CreateMap<CandidateDTO, ScheduleCandidateInfoDTO>()
               .ForMember(dest => dest.ProjectResult, opt => opt.Ignore())
               .ForMember(dest => dest.Skype, conf => conf.MapFrom(src => src.Contacts.FirstOrDefault(c => c.Type == DataAccess.Constants.Contacts.Skype).Value));

            CreateMap<StatusDTO, Status>();
        }
    }
}
