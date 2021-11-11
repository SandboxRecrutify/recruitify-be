using System.Linq;
using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class CreateCandidateValidator : AbstractValidator<CandidateCreateDTO>
    {
        private const string Skype = "Skype";

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
            RuleFor(c => c.Contacts)
                .Must(c => c.Any(contact => contact.Type == Skype))
                .WithMessage("Skype is required");
            RuleForEach(c => c.Contacts)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Location)
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
