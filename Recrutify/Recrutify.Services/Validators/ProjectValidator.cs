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
                .MaximumLength(128)
                .NotEmpty();
            RuleFor(p => p.Description)
                .NotNull()
                .MaximumLength(500)
                .NotEmpty();
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
        }
    }
}
