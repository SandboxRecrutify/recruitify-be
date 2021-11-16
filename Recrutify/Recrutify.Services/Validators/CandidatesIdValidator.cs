using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class CandidatesIdValidator : AbstractValidator<CreateBullFeedbackTestDTO>
    {
        public CandidatesIdValidator(ICandidateRepository candidateRepository)
        {
            RuleFor(x => x)
                .NotNull()
                .MustAsync(async (model, cancellation) => await CheckedId(candidateRepository, model))
                .WithMessage("Error. One or more candidates doesn't exist");
            RuleFor(x => x.Rating)
                .NotNull()
                .Must(r => r >= 0 && r <= 10)
                .WithMessage("Error. Rating is out of range");
        }

        private static async Task<bool> CheckedId(ICandidateRepository candidateRepository, CreateBullFeedbackTestDTO model)
        {
            var candidates = await candidateRepository.GetByIdsAsync(model.CandidatesIds);
            var result = model.CandidatesIds
                                    .All(x => candidates
                                        .Where(c => c.ProjectResults
                                            .Where(p => p.ProjectId == model.ProjectId)
                                            .Any(y => !y.Feedbacks
                                                .Any(f => f.Type == FeedbackType.Test)))
                                        .Select(i => i.Id).Contains(x));
            return result;
        }
    }
}
