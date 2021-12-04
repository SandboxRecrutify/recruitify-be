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

        public UpdateProjectValidator(IProjectRepository projectRepository, IUserRepository userRepository, IPrimarySkillRepository primarySkillRepository)
            : base(userRepository, primarySkillRepository)
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
        }

        private async Task UmmutableFieldsAreKeepingUnchanged(UpdateProjectDTO dto, ValidationContext<UpdateProjectDTO> context, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(dto.Id);
            if (project == null)
            {
               context.AddFailure("Project doesn't exist");
               return;
            }

            var currentPrimarySkillsIds = project.PrimarySkills.Select(x => x.Id).ToList();
            var primarySkillIds = dto.PrimarySkills.Select(x => x.Id).ToList();
            if (dto.Name != project.Name)
            {
                context.AddFailure("Name cannot be changed!");
            }

            if (dto.StartDate != project.StartDate)
            {
                context.AddFailure("StartDate cannot be changed!");
            }

            if (!primarySkillIds.SequenceEqual(currentPrimarySkillsIds))
            {
                context.AddFailure("PrimarySkills cannot be changed!");
            }
        }
    }
}
