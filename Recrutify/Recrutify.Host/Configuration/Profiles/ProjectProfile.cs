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
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CurrentApplicationsCount, opt => opt.Ignore());
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<StaffDTO, Staff>().ReverseMap();
            CreateMap<ProjectPrimarySkill, ProjectPrimarySkillDTO>().ReverseMap();
            CreateMap<PrimarySkill, PrimarySkillDTO>();
            CreateMap<User, StaffDTO>()
                .ForMember(dest => dest.UserId, conf => conf.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, conf => conf.MapFrom(src => $"{src.Name} {src.Surname}"));
        }
    }
}
