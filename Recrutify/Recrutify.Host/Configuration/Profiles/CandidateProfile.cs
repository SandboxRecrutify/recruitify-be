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

            CreateMap<Candidate, ScheduleCandidateInfoDTO>()
               .ForMember(dest => dest.ProjectResult, opt => opt.Ignore());

            CreateMap<CandidateCreateDTO, Candidate>()
                .ForMember(dest => dest.ProjectResults, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, conf => conf.MapFrom(src => DateTime.UtcNow.Date))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CandidatePrimarySkill, CandidatePrimarySkillDTO>().ReverseMap();

            CreateMap<CandidatesProjectInfo, CandidatesProjectInfoDTO>();

            CreateMap<ProjectResult, ProjectResultDTO>()
                .ForMember(dest => dest.PrimarySkill, conf => conf.MapFrom(src => new CandidatePrimarySkillDTO { Id = src.PrimarySkill.Id, Name = src.PrimarySkill.Name }))
                .ForMember(dest => dest.IsAssigned, conf => conf.MapFrom(src => false))
                .ForMember(dest => dest.TestRating, opt => opt.Ignore())
                .ForMember(dest => dest.MentorFeedbackRating, opt => opt.Ignore())
                .ForMember(dest => dest.InterviewRating, opt => opt.Ignore())
                .ForMember(dest => dest.TechInterviewOneStepRating, opt => opt.Ignore())
                .ForMember(dest => dest.TechInterviewSecondStepRating, opt => opt.Ignore());

            CreateMap<ProjectResult, ScheduleCandidateProjectResultDTO>()
                .ForMember(dest => dest.PrimarySkill, conf => conf.MapFrom(src => new CandidatePrimarySkillDTO { Id = src.PrimarySkill.Id, Name = src.PrimarySkill.Name }));

            CreateMap<UpsertFeedbackDTO, Feedback>()
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore());
            CreateMap<Feedback, FeedbackDTO>();

            CreateMap<ContactDTO, Contact>().ReverseMap();

            CreateMap<LocationDTO, Location>().ReverseMap();
        }
    }
}
