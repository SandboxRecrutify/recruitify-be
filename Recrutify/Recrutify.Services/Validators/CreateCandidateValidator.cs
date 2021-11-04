using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public abstract class CreateCandidateValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CandidateCreateDTO
    {
        public CreateCandidateValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\+?(\d[\d\- ]+)?(\(?[\d\- ]+\))?([\d\- ])+\d$");
            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.Contacts)
                .NotNull()
                .NotEmpty();
            RuleForEach(c => c.Contacts)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Location)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.PrimarySkills)
                .NotNull()
                .NotEmpty();
            RuleForEach(c => c.PrimarySkills)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.BestTimeToConnect)
                .NotNull()
                .NotEmpty();
            RuleForEach(c => c.BestTimeToConnect)
                .NotEmpty();
        }
    }
}
