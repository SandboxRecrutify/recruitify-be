using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public abstract class BaseCandidateValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CandidateCreateDTO
    {
        public BaseCandidateValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(c => c.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(c => c.EnglishLevel)
                .NotNull();
            RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty();
            // проверка на отсутсвие букв
            RuleFor(c => c.Email)
               .NotNull()
               .NotEmpty();
            // проверка на соответствие электронной почте
            RuleFor(c => c.Contacts)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Location)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.PrimarySkills)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.RegistrationDate)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.BestTimeToConnect)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.GoingToExadel)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.ProjectResults)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.CurrentJob)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Certificates)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.AdditionalQuestions)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.AdditionalInfo)
                .NotNull();

        }
    }
}
