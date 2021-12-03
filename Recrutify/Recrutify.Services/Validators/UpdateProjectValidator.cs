using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpdateProjectValidator : BaseProjectValidator<UpdateProjectDTO>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectValidator(IProjectRepository projectRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _projectRepository = projectRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p)
                .CustomAsync(UmmutableFieldsAreKeepingUnchanged);
            RuleFor(p => p.Id)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date must be in the today or future!");
        }

        private async Task UmmutableFieldsAreKeepingUnchanged(UpdateProjectDTO dto, ValidationContext<UpdateProjectDTO> context, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(dto.Id);
            if (project == null)
            {
                throw new ValidationException("Primary skill does't exist!");
            }

            var currentPrimarySkills = project.PrimarySkills.Select(x => x.Id).ToList();
            var primarySkillIds = dto.PrimarySkills.Select(x => x.Id).ToList();
            if (dto.Name != project.Name)
            {
                context.AddFailure("Name cannot be changed!");
            }

            if (dto.StartDate != project.StartDate)
            {
                context.AddFailure("StartDate cannot be changed!");
            }

            if (!primarySkillIds.SequenceEqual(currentPrimarySkills))
            {
                context.AddFailure("PrimarySkills cannot be changed!");
            }
        }
    }
}
