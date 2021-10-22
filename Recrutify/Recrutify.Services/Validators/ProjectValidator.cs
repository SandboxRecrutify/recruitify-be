using System;
using FluentValidation;
using Recrutify.Services.Dtos;

namespace Recrutify.Services.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectCreateDTO>
    {
        public ProjectValidator()
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
                .WithMessage("The date must be greater than the start date!");
            RuleFor(p => p.CurrentApplicationsCount)
                .NotEmpty();
            RuleFor(p => p.PlannedApplicationsCount)
                .NotEmpty();
            RuleFor(p => p.PrimarySkills)
                .NotNull()
                .NotEmpty();
            RuleForEach(p => p.Mentors)
                .NotNull()
                .NotEmpty()
                .ChildRules(orders =>
                {
                    orders.RuleFor(x => x.UserName)
                    .NotNull()
                    .NotEmpty();
                });
            RuleForEach(p => p.Managers)
                .NotNull()
                .NotEmpty()
                .ChildRules(orders =>
                {
                    orders.RuleFor(x => x.UserName)
                    .NotNull()
                    .NotEmpty();
                });
            RuleForEach(p => p.Interviewers)
                .NotNull()
                .NotEmpty()
                .ChildRules(orders =>
                {
                    orders.RuleFor(x => x.UserName)
                    .NotNull()
                    .NotEmpty();
                });
            RuleForEach(p => p.Recruters)
                .NotNull()
                .NotEmpty()
                .ChildRules(orders =>
                {
                    orders.RuleFor(x => x.UserName)
                    .NotNull()
                    .NotEmpty();
                });
            RuleFor(p => p.IsRecommended)
                .NotNull();
        }
    }
}
