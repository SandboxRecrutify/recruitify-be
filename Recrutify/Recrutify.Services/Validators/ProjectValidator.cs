using System;
using FluentValidation;
using Recrutify.Services.Dtos;

namespace Recrutify.Services.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectDTO>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.Description).NotEmpty()
                .WithMessage("Please describe the description");
            RuleFor(p => p.StartDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Date must be in the future");
            RuleFor(p => p.EndDate).NotNull();
            RuleFor(p => p.CurrentApplicationsCount).NotNull();
            RuleFor(p => p.PlannedApplicationsCount).NotNull();
            RuleForEach(p => p.PrimarySkills)
                .NotEmpty()
                .WithMessage("Values in the PrimarySkills can't be empty");
        }
    }
}
