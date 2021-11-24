using System;

namespace Recrutify.Services.DTOs
{
    public class UpdateProjectDTO : CreateProjectDTO
    {
        public Guid Id { get; set; }
    }
}
