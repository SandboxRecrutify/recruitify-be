using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectDTO>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}
