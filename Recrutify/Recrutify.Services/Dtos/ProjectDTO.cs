using System;

namespace Recrutify.Services.Dtos
{
    public class ProjectDto : ProjectCreateDto
    {
        public Guid Id { get; set; }
    }
}
