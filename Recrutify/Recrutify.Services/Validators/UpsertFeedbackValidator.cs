using System.Linq;
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
            RuleFor(f => f.Type)
                .IsInEnum()
                .WithMessage("Type doesn't exist")
                .Must(t => t != FeedbackTypeDTO.Test)
                .WithMessage("Cannot change to selected type manually");
        }
    }
}
