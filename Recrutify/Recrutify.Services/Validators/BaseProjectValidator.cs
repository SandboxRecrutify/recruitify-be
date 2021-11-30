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
        protected BaseProjectValidator()
        {
            RuleFor(p => p)
                .CustomAsync(CheckStuffAsync);
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

        protected IUserRepository UserRepository { get; set; }

        protected IEnumerable<User> Users { get; set; }

        protected async Task CheckStuffAsync(TDTO projectDTO, ValidationContext<TDTO> context, CancellationToken cancellation)
        {
            var userIds = GetStuffIds(projectDTO);
            var userIdsWithoutDublicates = DeleteDublicates(userIds);
            if (!await UserRepository.ExistsByIdsAsync(userIdsWithoutDublicates, cancellation))
            {
                context.AddFailure("User doesn't exist");
            }
        }

        private IEnumerable<Guid> DeleteDublicates(IEnumerable<Guid> userIds)
        {
            return userIds
                .GroupBy(userId => userId)
                .Where(group => group.Count() > 1)
                .Select(id => id.Key);
        }

        private IEnumerable<Guid> GetStuffIds(TDTO projectDTO)
        {
            var stuffIds = new List<Guid>();
            foreach (var userId in projectDTO.Managers)
            {
                stuffIds.Add(userId);
            }

            foreach (var userId in projectDTO.Mentors)
            {
                stuffIds.Add(userId);
            }

            foreach (var userId in projectDTO.Recruiters)
            {
                stuffIds.Add(userId);
            }

            foreach (var userId in projectDTO.Interviewers)
            {
                stuffIds.Add(userId);
            }

            return stuffIds;
        }
    }
}
