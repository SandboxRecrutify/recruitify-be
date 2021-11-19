using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public abstract class BaseCandidateDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public EnglishLevelDTO EnglishLevel { get; set; }

        public ProjectLanguageDTO ProjectLanguage { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IEnumerable<ContactDTO> Contacts { get; set; }

        public LocationDTO Location { get; set; }

        public IEnumerable<int> BestTimeToConnect { get; set; }

        public bool GoingToExadel { get; set; }

        public string CurrentJob { get; set; }

        public string Certificates { get; set; }

        public string AdditionalQuestions { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
