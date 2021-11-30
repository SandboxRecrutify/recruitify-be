using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class BulkAppointInterviewsDTO
    {
        public IEnumerable<AppointInterviewDTO> AppointInterviewDTOs { get; set; }
    }
}
