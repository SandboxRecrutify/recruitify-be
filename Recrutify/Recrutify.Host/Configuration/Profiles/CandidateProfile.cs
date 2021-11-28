﻿using System;
using System.Collections.Generic;
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
                .ForMember(dest => dest.ProjectResults, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, conf => conf.MapFrom(src => DateTime.UtcNow.Date))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProjectResult, ProjectResultDTO>()
                .ForMember(dest => dest.PrimarySkill, conf => conf.MapFrom(src => new CandidatePrimarySkillDTO { Id = src.PrimarySkill.Id, Name = src.PrimarySkill.Name }))
                .ForMember(dest => dest.IsAssigned, conf => conf.MapFrom(src => false));
            CreateMap<ProjectResult, ScheduleCandidateProjectResultDTO>().ReverseMap()
                .ForMember(dest => dest.PrimarySkill, conf => conf.MapFrom(src => new CandidatePrimarySkillDTO { Id = src.PrimarySkill.Id, Name = src.PrimarySkill.Name }));
            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();

            CreateMap<UpsertFeedbackDTO, Feedback>()
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore());
            CreateMap<Feedback, FeedbackDTO>();

            CreateMap<ContactDTO, Contact>().ReverseMap();

            CreateMap<LocationDTO, Location>().ReverseMap();

            CreateMap<Candidate, ScheduleCandidateInfoDTO>()
                .ForMember(dest => dest.ProjectResult, opt => opt.Ignore());

            CreateMap<CandidatesPrimarySkillsAndLocation, CandidatesPrimarySkillsAndLocationDTO>();
               // .ForMember(dest => dest.Locations, conf => conf.MapFrom(src => src.Locations))
               // .ForMember(dest => dest.PrimarySkills, conf => conf.MapFrom(src => src.PrimarySkills));
            
        }
    }
}
