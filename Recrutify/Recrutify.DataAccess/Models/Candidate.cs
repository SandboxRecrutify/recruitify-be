using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class Candidate : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public EnglishLevel EnglishLevel { get; set; }

        public ProjectLanguage ProjectLanguage { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }

        public Location Location { get; set; }

        public DateTime RegistrationDate { get; set; }

        public IEnumerable<int> BestTimeToConnect { get; set; }

        public bool GoingToExadel { get; set; }

        public IEnumerable<ProjectResult> ProjectResults { get; set; }

        public string CurrentJob { get; set; }

        public string Certificates { get; set; }

        public string AdditionalQuestions { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
