using System.Linq;
using FluentValidation;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkUpdateStatusReasonCandidatsValidator : AbstractValidator<BulkUpdateStatusDTO>
    {
        private readonly ICandidateRepository _candidateRepository;

        public BulkUpdateStatusReasonCandidatsValidator(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(x => x.CandidatesIds)
                .NotNull()
                .NotEmpty()
                .MustAsync(_candidateRepository.ExistsByIdsAsync)
                .WithMessage("One or more candidates doesn't exist");
            RuleFor(x => x.Reason)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .When(x => x.Status == StatusDTO.Declined)
                .WithMessage("Reason is mandatory if status is declined");
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status doesn't exist")
                .Must(s => new[] { StatusDTO.Accepted, StatusDTO.Declined, StatusDTO.WaitingList, }.Contains(s))
                .WithMessage("Cannot change to selected status manually");
        }
    }
}
