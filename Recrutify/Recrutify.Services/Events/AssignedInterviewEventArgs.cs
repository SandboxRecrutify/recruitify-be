using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Events
{
    public class AssignedInterviewEventArgs
    {
        public IEnumerable<Interview> Interviews { get; set; }
    }
}
