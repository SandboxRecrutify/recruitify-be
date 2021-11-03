using System;
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
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));

            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();
            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<LocationDTO, Location>().ReverseMap();
            CreateMap<ProjectResult, ProjectResultDTO>().ReverseMap();
            CreateMap<Feedback, FeedbackDTO>().ReverseMap();

            CreateMap<Status, StatusDTO>()
                .ConvertUsing(x => (StatusDTO)((int)x));
            CreateMap<StatusDTO, Status>();

            CreateMap<FeedbackType, FeedbackTypeDTO>()
                .ConvertUsing(x => (FeedbackTypeDTO)((int)x));
            CreateMap<FeedbackTypeDTO, FeedbackType>();

            CreateMap<EnglishLevel, EnglishLevelDTO>()
                .ConvertUsing(x => (EnglishLevelDTO)((int)x));
            CreateMap<EnglishLevelDTO, EnglishLevel>();
        }
    }
}
