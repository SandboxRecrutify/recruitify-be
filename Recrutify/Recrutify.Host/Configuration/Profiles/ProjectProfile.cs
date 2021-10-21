using System;
using Recrutify.DataAccess;
using Recrutify.Services.Dtos;

namespace Recrutify.Host.Configuration.Profiles
{
    public class ProjectProfile : AutoMapper.Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDto, Project>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
            CreateMap<Project, ProjectDto>();
        }
    }
}
