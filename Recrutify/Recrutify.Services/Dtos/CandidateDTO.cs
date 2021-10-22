using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Dtos
{
    class CandidateDTO : CandidateCreateDTO
    {
        public Guid Id { get; set; }
    }
}
