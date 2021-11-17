using System.Linq;
using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkUpdateStatusReasonValidator : AbstractValidator<BulkUpdateStatusDTO>
    {
        public BulkUpdateStatusReasonValidator()
        {
            RuleFor(x => x.CandidatesIds)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Reason)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .When(x => x.Status == StatusDTO.Declined);
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Choose from existing statuses")
                .Must(s => new[] { StatusDTO.Accepted, StatusDTO.Declined, StatusDTO.WaitingList, }.Contains(s))
                .WithMessage("The selected status is not valid");
        }
    }
}
