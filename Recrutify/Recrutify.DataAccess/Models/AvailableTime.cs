using System;

namespace Recrutify.DataAccess.Models
{
    public class AvailableTime
    {
        public DateTime Date { get; set; }

        public InfoCandidat Candidat { get; set; }
    }
}
