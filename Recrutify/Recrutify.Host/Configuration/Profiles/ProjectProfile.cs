using System;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration.Profiles
{
    public class ProjectProfile : AutoMapper.Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectDTO, Project>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<StaffDTO, Staff>().ReverseMap();
            CreateMap<PrimarySkillDTO, PrimarySkill>().ReverseMap();
        }
    }
}
