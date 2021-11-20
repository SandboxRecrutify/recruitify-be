using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class StaffByProject
    {
        public Guid ProjectId { get; set; }

        public IEnumerable<Staff> Managers { get; set; }

        public IEnumerable<Staff> Interviewers { get; set; }

        public IEnumerable<Staff> Recruiters { get; set; }

        public IEnumerable<Staff> Mentors { get; set; }
    }
}
