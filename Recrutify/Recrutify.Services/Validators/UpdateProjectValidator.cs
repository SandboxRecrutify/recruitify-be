using System;
using FluentValidation;
using Recrutify.DataAccess;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpdateProjectValidator : BaseProjectValidator<UpdateProjectDTO>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date must be in the today or future!");
            
        }
    }
}
