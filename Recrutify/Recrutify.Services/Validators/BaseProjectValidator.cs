using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public abstract class BaseProjectValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CreateProjectDTO
    {
        protected BaseProjectValidator(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            ConfigureRules();
        }

        private IUserRepository UserRepository { get; set; }

        private void ConfigureRules()
        {
            RuleFor(p => p)
                 .MustAsync(CheckStaffAsync)
                 .WithMessage("User isn't found");
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
                .WithMessage("The date must be greater than the start date!");
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
                .NotEmpty();
            RuleForEach(p => p.PrimarySkills)
                .NotNull()
                .NotEmpty();
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
    }
}
