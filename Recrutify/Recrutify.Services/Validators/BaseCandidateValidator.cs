using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public abstract class BaseCandidateValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CandidateCreateDTO
    {
        public BaseCandidateValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(c => c.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(c => c.EnglishLevel)
                .NotNull();
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
            RuleFor(c => c.RegistrationDate)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.BestTimeToConnect)
                .NotNull()
                .NotEmpty();
            RuleForEach(c => c.BestTimeToConnect)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.GoingToExadel)
                .NotNull();
        }
    }
}
