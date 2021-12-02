using System.Collections.Generic;
using Recrutify.Services.EmailModels;

namespace Recrutify.Services.Events
{
    public class AssignedInterviewEventArgs
    {
        public IEnumerable<Interview> Interviews { get; set; }
    }
}
