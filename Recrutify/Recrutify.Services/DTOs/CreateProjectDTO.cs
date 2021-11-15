using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CreateProjectDTO : BaseProjectDTO
    {
        public IEnumerable<Guid> Managers { get; set; }

        public IEnumerable<Guid> Interviewers { get; set; }

        public IEnumerable<Guid> Recruiters { get; set; }

        public IEnumerable<Guid> Mentors { get; set; }
    }
}
