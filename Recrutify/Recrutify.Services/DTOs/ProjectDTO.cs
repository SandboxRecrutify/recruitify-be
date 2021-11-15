using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class ProjectDTO : BaseProjectDTO
    {
        public Guid Id { get; set; }

        public IEnumerable<StaffDTO> Managers { get; set; }

        public IEnumerable<StaffDTO> Interviewers { get; set; }

        public IEnumerable<StaffDTO> Recruiters { get; set; }

        public IEnumerable<StaffDTO> Mentors { get; set; }
    }
}
