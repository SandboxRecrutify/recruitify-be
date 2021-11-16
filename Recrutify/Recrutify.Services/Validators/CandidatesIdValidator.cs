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
    public class CandidatesIdValidator : AbstractValidator<BulkCreateTestFeedbackDTO>
    {
        readonly ICandidateRepository _candidateRepository;

        public CandidatesIdValidator(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;

            RuleFor(x => x)
                .NotNull()
                .MustAsync(CandidatesAreExistingAsync)
                .WithMessage("One or more candidates doesn't exist");
            RuleFor(x => x.Rating)
                .NotNull()
                .Must(r => r >= 0 && r <= 10)
                .WithMessage("Rating is out of range");
        }

        private async Task<bool> CandidatesAreExistingAsync(BulkCreateTestFeedbackDTO dto, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(dto.CandidatesIds);
            var filteredcandidatesIds = candidates.Where(c => c.ProjectResults
                                                   .Where(p => p.ProjectId == dto.ProjectId)
                                                   .Any(p => !p.Feedbacks
                                                       .Any(f => f.Type == FeedbackType.Test)))
                                               .Select(i => i.Id).ToList();
            var result = dto.CandidatesIds.All(x => filteredcandidatesIds.Contains(x));
            return result;
        }
    }
}
