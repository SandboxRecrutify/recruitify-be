using System;
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
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();
            CreateMap<ProjectResultDTO, ProjectResult>().ReverseMap();
            CreateMap<FeedbackDTO, Feedback>().ReverseMap();
            CreateMap<FeedbackTypeDTO, FeedbackType>().ReverseMap();
            CreateMap<StatusDTO, Status>().ReverseMap();
            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<EnglishLevelDTO, EnglishLevel>().ReverseMap();
            CreateMap<LocationDTO, Location>().ReverseMap();
        }
    }
}
