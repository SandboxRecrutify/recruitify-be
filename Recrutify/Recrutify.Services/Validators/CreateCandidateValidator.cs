﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class CreateCandidateValidator : AbstractValidator<CandidateCreateDTO>
    {
        private const string Skype = "Skype";

        private readonly IPrimarySkillRepository _primarySkillRepository;

        public CreateCandidateValidator(IPrimarySkillRepository primarySkillRepository)
        {
            _primarySkillRepository = primarySkillRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\+?(\d[\d\- ]+)?(\(?[\d\- ]+\))?([\d\- ])+\d$");
            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.Contacts)
               .NotNull()
               .NotEmpty()
               .Must(c => c.Any(contact => contact.Type == Skype))
               .WithMessage("Skype is required")
               .Must(c => c.Count() <= 5)
               .WithMessage("Maximum contacts reached");
            RuleForEach(c => c.Contacts)
                .NotNull()
                .NotEmpty()
                .ChildRules(x => x.RuleFor(x => x.Type).MaximumLength(50))
                .ChildRules(x => x.RuleFor(x => x.Value).MaximumLength(50));
            RuleFor(c => c.Location.City)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.Location.Country)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.BestTimeToConnect)
                .NotNull()
                .NotEmpty();
            RuleForEach(c => c.BestTimeToConnect)
                .NotEmpty()
                .Must(b => b >= 9 && b <= 18)
                .WithMessage("Time is out of range");
            RuleFor(c => c.PrimarySkillId)
                .NotEmpty()
                .MustAsync(_primarySkillRepository.ExistsIdAsync)
                .WithMessage("Primary Skill doesn't exist");
            RuleFor(c => c.CurrentJob)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.Certificates)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);
            RuleFor(c => c.AdditionalQuestions)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);
            RuleFor(c => c.AdditionalInfo)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);
            RuleFor(c => c.EnglishLevel)
                .IsInEnum()
                .NotEmpty();
            RuleFor(c => c.ProjectLanguage)
                .IsInEnum()
                .NotEmpty();
        }
    }
}
