using System;
using System.Linq;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class CandidatePrimarySkillValidator : AbstractValidator<CandidatePrimarySkillDTO>
    {
        public CandidatePrimarySkillValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}
