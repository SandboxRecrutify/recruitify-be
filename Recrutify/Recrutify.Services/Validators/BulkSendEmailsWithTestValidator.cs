using FluentValidation;
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
                .MustAsync(_candidateRepository.ExistsByIdsAsync)
                .WithMessage("One or more candidates doesn't exist");
        }

    }
}
