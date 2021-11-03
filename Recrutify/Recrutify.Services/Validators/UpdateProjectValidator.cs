using System;
using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpdateProjectValidator : BaseProjectValidator<ProjectDTO>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
