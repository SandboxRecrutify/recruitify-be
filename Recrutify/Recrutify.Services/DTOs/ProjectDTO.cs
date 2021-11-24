using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.DTOs
{
    public class ProjectDTO : BaseProjectDTO
    {
        public Guid Id { get; set; }

        public IEnumerable<Staff> Managers { get; set; }

        public IEnumerable<Staff> Interviewers { get; set; }

        public IEnumerable<Staff> Recruiters { get; set; }

        public IEnumerable<Staff> Mentors { get; set; }
    }
}
