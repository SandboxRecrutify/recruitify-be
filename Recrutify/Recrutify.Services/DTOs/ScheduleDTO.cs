using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class ScheduleDTO
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public UserPrimarySkillDTO UserPrimarySkill { get; set; }

        public IEnumerable<ScheduleSlotDTO> ScheduleSlots { get; set; }
    }
}
