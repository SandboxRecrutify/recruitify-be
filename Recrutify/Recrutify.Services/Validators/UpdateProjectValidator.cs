using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpdateProjectValidator : BaseProjectValidator<UpdateProjectDTO>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }
}
