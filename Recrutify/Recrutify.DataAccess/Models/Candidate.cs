using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess
{
    public class Candidate : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public EnglishLevel EnglishLevel { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<Contact> Contacts { get; set; }

        public Location Location { get; set; }

        public List<PrimarySkill> PrimarySkills { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<int> BestTimeToContact { get; set; }

        public bool GoingToExadel { get; set; }

        public List<CourseResult> CoursesResults { get; set; }

        public string CurrentJob { get; set; }

        public string Certificates { get; set; }

        public string AdditionalQuestions { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
