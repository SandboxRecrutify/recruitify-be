using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class ProjectResult : BaseProjectResult
    {
        public IEnumerable<Feedback> Feedbacks { get; set; }

        public string Reason { get; set; }
    }
}
