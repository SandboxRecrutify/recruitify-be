using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public abstract class BaseProjectValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CreateProjectDTO
    {
        protected BaseProjectValidator(IUserRepository userRepository, IPrimarySkillRepository primarySkillRepository)
        {
            PrimarySkillRepository = primarySkillRepository;
            UserRepository = userRepository;
            ConfigureRules();
        }

        private IUserRepository UserRepository { get; set; }

        private IPrimarySkillRepository PrimarySkillRepository { get; set; }

        private void ConfigureRules()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(p => p.StartDate)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date must be in the today or future!");
            RuleFor(p => p.EndDate)
                .NotNull()
                .GreaterThan(p => p.StartDate)
                .WithMessage("The date must be greater than the start date, must be in the today or future!");
            RuleFor(p => p.StartRegistrationDate)
               .NotNull()
               .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
               .WithMessage("Date must be in the today or future!");
            RuleFor(p => p.EndRegistrationDate)
                .NotNull()
                .GreaterThan(p => p.StartRegistrationDate)
                .WithMessage("The date must be greater than the start registration date!")
                .LessThanOrEqualTo(p => p.StartDate)
                .WithMessage("The date must be less than the start date!");
            RuleFor(p => p.PlannedApplicationsCount)
                .NotEmpty();
            RuleFor(p => p.PrimarySkills)
                .NotNull()
                .NotEmpty()
                .MustAsync(CheckPrimarySkillsAsync);
            RuleForEach(p => p.PrimarySkills)
                .NotNull()
                .NotEmpty()
                .ChildRules(x => x.RuleFor(x => x.TestLink)
                                       .Must(LinkMustBeAUri)
                                       .WithMessage("The test link must be a valid URI!"));
            RuleFor(p => p.Mentors)
                 .NotNull()
                 .NotEmpty();
            RuleForEach(p => p.Mentors)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Managers)
                 .NotNull()
                 .NotEmpty();
            RuleForEach(p => p.Managers)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Interviewers)
                 .NotNull()
                 .NotEmpty();
            RuleForEach(p => p.Interviewers)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Recruiters)
                .NotNull()
                .NotEmpty();
            RuleForEach(p => p.Recruiters)
               .NotNull()
               .NotEmpty();
            RuleFor(p => p)
                 .MustAsync(CheckStaffAsync)
                 .WithMessage("User isn't found");
        }

        private async Task<bool> CheckStaffAsync(TDTO projectDTO, CancellationToken cancellation)
        {
            var userIds = GetStaffIds(projectDTO);
            return await UserRepository.ExistsByIdsAsync(userIds, cancellation);
        }

        private IEnumerable<Guid> GetStaffIds(TDTO projectDTO)
        {
            var stuffIds = projectDTO.Interviewers
                .Union(projectDTO.Mentors
                .Union(projectDTO.Managers
                .Union(projectDTO.Recruiters)));
            return stuffIds;
        }

        private Task<bool> CheckPrimarySkillsAsync(IEnumerable<ProjectPrimarySkillDTO> primarySkillDTOs, CancellationToken cancellation)
        {
            var ids = primarySkillDTOs.Select(x => x.Id).ToList();
            return PrimarySkillRepository.ExistsByIdsAsync(ids, cancellation);
        }

        private bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            return Uri.TryCreate(link, UriKind.Absolute, out var result);
        }
    }
}
