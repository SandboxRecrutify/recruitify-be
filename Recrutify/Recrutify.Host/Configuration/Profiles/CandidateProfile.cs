﻿using System;
using System.Linq;
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
                .ForMember(dest => dest.ProjectResult, opt => opt.Ignore())
                .ForMember(dest => dest.Skype, conf => conf.MapFrom(src => src.Contacts.FirstOrDefault(c => c.Type == DataAccess.Constants.Contacts.Skype).Value));
            CreateMap<CandidateDTO, ScheduleCandidateInfoDTO>()
               .ForMember(dest => dest.ProjectResult, opt => opt.Ignore())
               .ForMember(dest => dest.Skype, conf => conf.MapFrom(src => src.Contacts.FirstOrDefault(c => c.Type == DataAccess.Constants.Contacts.Skype).Value));

            CreateMap<CandidatesProjectInfo, CandidatesProjectInfoDTO>();
            CreateMap<InterviewAppointmentDTO, CandidateRenewal>()
                .ForMember(dest => dest.CandidateId, conf => conf.MapFrom(src => src.CandidateId))
                .ForMember(dest => dest.IsAssignedOnInterview, conf => conf.MapFrom(src => src.IsAppointment))
                .ForMember(dest => dest.Status, conf => conf.MapFrom(src => src.IsAppointment ? StatusDTO.TechInterviewOneStep : StatusDTO.RecruiterInterview));
        }
    }
}
