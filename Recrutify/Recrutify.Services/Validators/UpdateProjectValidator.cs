using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpdateProjectValidator : BaseProjectValidator<ProjectDTO>
    {
        public UpdateProjectValidator()
        {
            RuleFor(b => b.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
