using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkUpdateStatusReasonValidator : AbstractValidator<BulkUpdateStatusDTO>
    {
        private readonly ICandidateRepository _candidateRepository;

        public BulkUpdateStatusReasonValidator(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(x => x)
                .NotNull()
                .MustAsync(CandidatesAreExistingAsync)
                .WithMessage("One or more candidates doesn't exist");
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

        private async Task<bool> CandidatesAreExistingAsync(BulkUpdateStatusDTO dto, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(dto.CandidatesIds);
            var filteredCandidatesIds = candidates.Where(c => c.ProjectResults
                                                       .FirstOrDefault(p => p.ProjectId == dto.ProjectId)
                                                       ?.Feedbacks.All(f => f.Type != FeedbackType.Test) ?? false)
                                                   .Select(c => c.Id)
                                                   .ToList();
            return dto.CandidatesIds.All(id => filteredCandidatesIds.Contains(id));
        }
    }
}
