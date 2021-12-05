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
                .NotEmpty()
                .Must(r => r >= 1 && r <= 4)
                .WithMessage("Rating is out of range");
            RuleFor(f => f.Type)
                .IsInEnum()
                .WithMessage("Type doesn't exist")
                .Must(t => t != FeedbackTypeDTO.Test)
                .WithMessage("Cannot create/update test feedback");
        }
    }
}
