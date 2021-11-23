using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class PrimarySkillsAndStaffDTO
    {
        public List<PrimarySkillDTO> PrimarySkills { get; set; }

        public StaffGroupDTO StaffGroup { get; set; }
    }
}
