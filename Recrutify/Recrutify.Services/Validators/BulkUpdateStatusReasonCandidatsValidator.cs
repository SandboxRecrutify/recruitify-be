using System.Linq;
using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkUpdateStatusReasonCandidatsValidator : AbstractValidator<BulkUpdateStatusDTO>
    {
        public BulkUpdateStatusReasonCandidatsValidator()
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
                .Must(s => new[] { StatusDTO.Accepted, StatusDTO.Declined, StatusDTO.WaitingList, }.Contains(s))
                .WithMessage("Choose from existing statuses");
        }
    }
}
