using System;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration.Profiles
{ 
    public class CandidateProfile : AutoMapper.Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateCreateDTO, CandidateDTO>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
            CreateMap<Candidate, CandidateDTO>();
            CreateMap<CandidateCreateDTO, Candidate>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
            CreateMap<CandidatePrimarySkillDTO, CandidatePrimarySkill>().ReverseMap();
        }
    }
}
