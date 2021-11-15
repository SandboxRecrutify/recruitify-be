using System;
using System.Collections.Generic;
using System.Linq;
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
                .Must(testDTO => testDTO.CandidatesId
                                            .All(x => candidateRepository.GetManyCandidates(testDTO.CandidatesId)
                                                .Where(c => c.ProjectResults
                                                    .Where(p => p.ProjectId == testDTO.ProjectId)
                                                    .Any(y => !y.Feedbacks
                                                        .Any(f => f.Type == FeedbackType.Test)))
                                                .Select(i => i.Id).Contains(x)))
                .WithMessage("Error. Invalid Id list");
            RuleFor(x => x.Rating)
                .NotNull()
                .Must(r => r >= 0 && r <= 10)
                .WithMessage("Error. Incorrect value rating");
        }
    }
}
