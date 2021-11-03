using System;
using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public abstract class BaseFeedbackValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CreateFeedbackDTO
    {
        protected BaseFeedbackValidator()
        {
            RuleFor(f => f.TextFeedback)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(f => f.Rating)
                .NotEmpty();
            RuleFor(f => f.UserId)
                .NotEqual(Guid.Empty);
            RuleFor(f => f.Type)
                .NotNull()
                .NotEmpty();
        }
    }
}
