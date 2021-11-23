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
    public class UpsertFeedbackValidator : AbstractValidator<UpsertFeedbackDTO>
    {
        private readonly ICandidateRepository _candidateRepository;

        public UpsertFeedbackValidator(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(f => f.TextFeedback)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(f => f.Rating)
                .NotEmpty();
        }

       /* private async Task<bool> CandidatesNewAsync(BulkCreateTestFeedbackDTO dto, Guid candidatsId, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetAsync(candidatsId);
            var filteredCandidat = candidate?.ProjectResults.Where(p => p.ProjectId == dto.ProjectId && p.Status == Status.New).FirstOrDefault()
                                                       ?.Feedbacks.All(f => f.Type == FeedbackType.Test);
            return (bool)filteredCandidat;
        }*/
    }
}
