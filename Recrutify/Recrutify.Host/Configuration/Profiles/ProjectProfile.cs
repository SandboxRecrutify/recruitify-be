﻿using System;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.Dtos;

namespace Recrutify.Host.Configuration.Profiles
{
    public class ProjectProfile : AutoMapper.Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()))
                .ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<StaffDTO, Staff>().ReverseMap();
        }
    }
}
