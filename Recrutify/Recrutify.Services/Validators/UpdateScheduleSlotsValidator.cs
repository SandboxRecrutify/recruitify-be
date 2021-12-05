using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Validators
{
    public class UpdateScheduleSlotsValidator : AbstractValidator<IEnumerable<ScheduleSlot>>
    {
        public UpdateScheduleSlotsValidator()
        {
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(slots => slots)
                .NotNull()
                .NotEmpty()
                .Must(slots => slots.All(s => s.ScheduleCandidateInfo == null))
                .WithMessage("It is forbidden to delete occupied schedule slots.");
        }
    }
}
