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

            CreateMap<Candidate, ScheduleCandidateInfoDTO>()
               .ForMember(dest => dest.ProjectResult, opt => opt.Ignore());

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

            CreateMap<CandidatesProjectInfo, CandidatesProjectInfoDTO>();
            CreateMap<ProjectResultDTO, CandidateRenewal>()
                .ForMember(dest => dest.CandidateId, opt => opt.Ignore())
                .ForMember(dest => dest.IsAssignedOnInterview, conf => conf.MapFrom(src => !src.IsAssignedOnInterview))
                .ForMember(dest => dest.Status, conf => conf.MapFrom(src => src.IsAssignedOnInterview ? src.Status - 1 : src.Status + 1));
        }
    }
}
