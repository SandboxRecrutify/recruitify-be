using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkCreateTestFeedbackValidator : AbstractValidator<BulkCreateTestFeedbackDTO>
    {
        private readonly ICandidateRepository _candidateRepository;

        public BulkCreateTestFeedbackValidator(ICandidateRepository candidateRepository)
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
            RuleFor(x => x.Rating)
                .Must(r => r >= 0 && r <= 10)
                .WithMessage("Rating is out of range");
        }

        private async Task<bool> CandidatesAreExistingAsync(BulkCreateTestFeedbackDTO dto, CancellationToken cancellationToken)
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
