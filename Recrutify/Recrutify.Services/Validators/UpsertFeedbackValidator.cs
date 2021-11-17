using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpsertFeedbackValidator : AbstractValidator<UpsertFeedbackDTO>
    {
        public UpsertFeedbackValidator()
        {
            RuleFor(f => f.TextFeedback)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(f => f.Rating)
                .NotEmpty();
        }
    }
}
