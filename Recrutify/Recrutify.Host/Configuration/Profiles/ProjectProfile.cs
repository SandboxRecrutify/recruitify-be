using System;
using System.Linq;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Extensions;
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
                .ForMember(dest => dest.Managers, conf => conf.MapFrom(src => src.Managers.Select(m => new Staff() { UserId = m })))
                .ForMember(dest => dest.Mentors, conf => conf.MapFrom(src => src.Mentors.Select(m => new Staff() { UserId = m })))
                .ForMember(dest => dest.Interviewers, conf => conf.MapFrom(src => src.Interviewers.Select(i => new Staff() { UserId = i })))
                .ForMember(dest => dest.Recruiters, conf => conf.MapFrom(src => src.Recruiters.Select(r => new Staff() { UserId = r })));
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<StaffDTO, Staff>().ReverseMap();
            CreateMap<ProjectPrimarySkill, ProjectPrimarySkillDTO>().ReverseMap();
            CreateMap<PrimarySkill, PrimarySkillDTO>();
            CreateMap<User, StaffDTO>()
                .ForMember(dest => dest.UserId, conf => conf.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, conf => conf.MapFrom(src => src.GetFullName()));
            CreateMap<Project, ShortProjectDTO>();
        }
    }
}
