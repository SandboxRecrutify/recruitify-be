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
    public class BulkSendEmailsWithTestValidator : AbstractValidator<BulkSendEmailWithTestDTO>
    {
        private readonly ICandidateRepository _candidateRepository;

        public BulkSendEmailsWithTestValidator(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(x => x.CandidatesIds)
                .NotNull()
                .NotEmpty()
                .MustAsync(CandidatesAreExistingAsync)
                .WithMessage("One or more candidates doesn't exist on project with status New");
        }

        private async Task<bool> CandidatesAreExistingAsync(BulkSendEmailWithTestDTO dto, IEnumerable<Guid> candidatsIds, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(candidatsIds);
            int filteredCandidatesCount = candidates.Count(c => c.ProjectResults
                                                       ?.FirstOrDefault(p => p.ProjectId == dto.ProjectId)
                                                       ?.Status.Equals(Status.New) ?? false);
            return filteredCandidatesCount == candidatsIds.Count();
        }
    }
}
