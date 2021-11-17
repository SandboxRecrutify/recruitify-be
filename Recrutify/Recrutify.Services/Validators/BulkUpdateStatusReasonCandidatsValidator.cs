using FluentValidation;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkUpdateStatusReasonCandidatsValidator : AbstractValidator<BulkUpdateStatusDTO>
    {
        public BulkUpdateStatusReasonCandidatsValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("One or more candidates doesn't exist");
            RuleFor(x => x.Reason)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Choose from existing statuses");
        }
    }

}
