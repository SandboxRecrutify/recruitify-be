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
                .Must(BeAValidData)
                .WithMessage("Time for editing is over");
            RuleFor(f => f.Status)
                .IsInEnum()
                .Must(s => !new[] { Status.Accepted, Status.Declined, Status.WaitingList, }.Contains(s))
                .WithMessage("Status is not available for update");
        }

        protected bool BeAValidData(Feedback date)
        {
            var currentData = DateTime.Now.Date;
            var feedbackData = date.CreatedOn;
            var result = DateTime.Now.Date - feedbackData;
            return feedbackData <= currentData && feedbackData.Equals(result.Hours <= 24);
        }
    }
}
