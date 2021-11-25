using System;
using System.Collections.Generic;
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

        public UpdateProjectValidator(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date must be in the today or future!");
            RuleFor(p => p.Name)
                .MustAsync(ProjectNameAreExistingAsync)
                .WithMessage("Name cannot be changed!");
            RuleFor(p => p.StartDate)
                .MustAsync(ProjectStartDateAreExistingAsync)
                .WithMessage("StartDate cannot be changed!");
            RuleFor(p => p.PrimarySkills)
                .MustAsync(ProjectPrimarySkillsAreExistingAsync)
                .WithMessage("PrimarySkills cannot be changed!");
        }

        private async Task<bool> ProjectNameAreExistingAsync(UpdateProjectDTO dto, string name, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(dto.Id);
            return project.Name.Equals(name);
        }

        private async Task<bool> ProjectStartDateAreExistingAsync(UpdateProjectDTO dto, DateTime dateTime, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(dto.Id);
            return project.StartDate.Equals(dateTime);
        }

        private async Task<bool> ProjectPrimarySkillsAreExistingAsync(UpdateProjectDTO dto, IEnumerable<ProjectPrimarySkillDTO> primariSkills, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(dto.Id);
            var primariSkillIds = project.PrimarySkills.Select(p => p.Id).ToList();
            var primariSkillIdsDto = primariSkills.Select(p => p.Id);
            return primariSkillIds.SequenceEqual(primariSkillIdsDto);
        }
    }
}
