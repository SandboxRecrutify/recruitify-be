using System;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration.Profiles
{
    public class CandidateProfile : AutoMapper.Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();
            CreateMap<FeedbackTypeDTO, FeedbackType>().ReverseMap();
            CreateMap<StatusDTO, Status>().ReverseMap();
            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<LocationDTO, Location>().ReverseMap();
            CreateMap<EnglishLevelDTO, EnglishLevel>().ReverseMap();

            CreateMap<Candidate, CandidateDTO>()
                 .ForMember(m => m.EnglishLevel, opt => opt.MapFrom(t => (EnglishLevelDTO)t.EnglishLevel));
            CreateMap<CandidateCreateDTO, Candidate>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));

            CreateMap<ProjectResult, ProjectResultDTO>()
                .ForMember(m => m.Status, opt => opt.MapFrom(t => (StatusDTO)t.Status));
            CreateMap<ProjectResultDTO, ProjectResult>();

            CreateMap<Feedback, FeedbackDTO>()
                .ForMember(m => m.Type, opt => opt.MapFrom(t => (FeedbackTypeDTO)t.Type));
            CreateMap<FeedbackDTO, Feedback>();
}
    }
}
