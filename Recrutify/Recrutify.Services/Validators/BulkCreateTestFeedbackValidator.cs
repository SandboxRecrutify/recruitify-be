using System;
using System.Collections.Generic;
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
        private readonly IProjectRepository _projectRepository;

        public BulkCreateTestFeedbackValidator(ICandidateRepository candidateRepository, IProjectRepository projectRepository)
        {
            _candidateRepository = candidateRepository;
            _projectRepository = projectRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(x => x.ProjectId)
                .MustAsync(_projectRepository.ExistsAsync)
                .WithMessage("Project doesn't exist");
            RuleFor(x => x.CandidatesIds)
                .NotNull()
                .NotEmpty()
                .MustAsync(CandidatesAreExistingAsync)
                .WithMessage("One or more candidates doesn't exist on project with status Test and no test feedbacks");
            RuleFor(x => x.Rating)
                .Must(r => r >= 1 && r <= 10)
                .WithMessage("Rating is out of range");
        }

        private async Task<bool> CandidatesAreExistingAsync(BulkCreateTestFeedbackDTO dto, IEnumerable<Guid> candidatsIds, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(candidatsIds);
            var filteredCandidatesCount = candidates.Count(c => c.ProjectResults
                                                       ?.FirstOrDefault(p => p.ProjectId == dto.ProjectId && p.Status == Status.Test)
                                                       ?.Feedbacks.All(f => f.Type != FeedbackType.Test) ?? false);
            return filteredCandidatesCount == candidatsIds.Count();
        }
    }
}
