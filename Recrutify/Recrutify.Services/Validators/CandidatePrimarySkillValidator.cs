using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class CandidatePrimarySkillValidator : AbstractValidator<CandidatePrimarySkillDTO>
    {
        public CandidatePrimarySkillValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}
