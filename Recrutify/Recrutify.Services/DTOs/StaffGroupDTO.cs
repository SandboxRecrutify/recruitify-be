using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class StaffGroupDTO
    {
        public IEnumerable<StaffDTO> Managers { get; set; }

        public IEnumerable<StaffDTO> Recruiters { get; set; }

        public IEnumerable<StaffDTO> Interviewers { get; set; }

        public IEnumerable<StaffDTO> Mentors { get; set; }
    }
}
