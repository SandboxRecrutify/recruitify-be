using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class PrimarySkillsAndStaffDTO
    {
        public List<PrimarySkillDTO> PrimarySkills { get; set; }

        public StaffGroupDTO StaffGroup { get; set; }
    }
}
