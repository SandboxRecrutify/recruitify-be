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
            RuleFor(p => p.Feedbacks.FirstOrDefault())
                .NotNull()
                .Must(f => DateTime.Now.Day - f.CreatedOn.Day <= 1)
                .WithMessage("Сannot be updated from one day after creation");
            RuleFor(p => p.Status)
                .IsInEnum()
                .Must(s => !new[] { Status.Accepted, Status.Declined, Status.WaitingList, }.Contains(s))
                .WithMessage("Cannot be updated for candidate in current status");
        }
    }
}
