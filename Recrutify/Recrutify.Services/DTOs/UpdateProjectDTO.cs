using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class UpdateProjectDTO : CreateProjectDTO
    {
        public Guid Id { get; set; }
    }
}
