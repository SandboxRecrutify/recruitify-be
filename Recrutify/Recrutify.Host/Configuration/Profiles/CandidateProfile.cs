using System;
using Recrutify.DataAccess.Models;
using Recrutify.Services.Dtos;

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
        }
    }
}
