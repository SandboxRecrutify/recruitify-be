using System;
using AutoMapper;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration.Profiles
{
    public class CandidateProfile : AutoMapper.Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDTO>().ReverseMap();
            CreateMap<CandidateCreateDTO, Candidate>()
                .ForMember(x => x.ProjectResults, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, conf => conf.MapFrom(src => DateTime.UtcNow.Date))
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();
            CreateMap<ProjectResultDTO, ProjectResult>().ReverseMap();
            CreateMap<CreateFeedbackDTO, Feedback>()
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<FeedbackTypeDTO, FeedbackType>().ReverseMap();
            CreateMap<StatusDTO, Status>().ReverseMap();
            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<EnglishLevelDTO, EnglishLevel>().ReverseMap();
            CreateMap<LocationDTO, Location>().ReverseMap();
        }
    }
}
