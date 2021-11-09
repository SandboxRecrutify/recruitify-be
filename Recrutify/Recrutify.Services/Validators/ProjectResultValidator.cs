using System;
using System.Linq;
using FluentValidation;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Validators
{
    public class ProjectResultValidator : AbstractValidator<ProjectResult>
    {
        public ProjectResultValidator()
        {
            RuleFor(f => f.Feedbacks.FirstOrDefault())
                .NotNull()
                .Must(ShouldBeNoMoreAValidTime)
                .WithMessage("Сannot be updated from one day after creation");
            RuleFor(f => f.Status)
                .IsInEnum()
                .Must(s => !new[] { Status.Accepted, Status.Declined, Status.WaitingList, }.Contains(s))
                .WithMessage("Cannot be updated for candidate in current status");
        }

        protected bool ShouldBeNoMoreAValidTime(Feedback date)
        {
            var feedbackCreationDate = date.CreatedOn;
            var result = DateTime.Now - feedbackCreationDate;
            return result.Hours <= 24;
        }
    }
}
